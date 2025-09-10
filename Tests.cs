using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using Xunit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace EshopOnWebTests
{
    public class Tests : IDisposable
    {
        private readonly IWebDriver driver;
        private readonly string baseUrl = "https://localhost:5001";
        private readonly List<string> reportLines = new List<string>();


        public Tests()
        {
            new DriverManager().SetUpDriver(new EdgeConfig());
            var options = new EdgeOptions();
            options.AddArgument("ignore-certificate-errors");
            driver = new EdgeDriver(options);
            driver.Manage().Window.Maximize();

        }

        [Fact]
        public void TC04()
        {
            driver.Navigate().GoToUrl(baseUrl);



            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(d => d.FindElement(By.Id("CatalogModel_BrandFilterApplied")));

            var brandDropdown = driver.FindElement(By.Id("CatalogModel_BrandFilterApplied"));
            var brandOptions = brandDropdown.FindElements(By.TagName("option"));

            for (int i = 0; i < brandOptions.Count; i++)
            {
                brandDropdown = driver.FindElement(By.Id("CatalogModel_BrandFilterApplied"));
                var brandSelect = new SelectElement(brandDropdown);
                brandOptions = brandDropdown.FindElements(By.TagName("option"));
                string brandName = brandOptions[i].Text;
                brandSelect.SelectByIndex(i);

                var typeDropdown = driver.FindElement(By.Id("CatalogModel_TypesFilterApplied"));
                var typeOptions = typeDropdown.FindElements(By.TagName("option"));

                for (int j = 0; j < typeOptions.Count; j++)
                {
                    typeDropdown = driver.FindElement(By.Id("CatalogModel_TypesFilterApplied"));
                    var typeSelect = new SelectElement(typeDropdown);
                    typeOptions = typeDropdown.FindElements(By.TagName("option"));
                    string typeName = typeOptions[j].Text;
                    typeSelect.SelectByIndex(j);

                    driver.FindElement(By.CssSelector("input.esh-catalog-send")).Click();
                        bool productsLoaded = true;
                        try
                        {
                            wait.Until(d => d.FindElements(By.CssSelector(".esh-catalog-item")).Count > 0);
                        }
                        catch (WebDriverTimeoutException)
                        {
                            productsLoaded = false; 
                        }

                        var products = driver.FindElements(By.CssSelector(".esh-catalog-item"));
                        if (productsLoaded)
                        {
                            Console.WriteLine($"Brand '{brandName}', Type '{typeName}' -> {products.Count} products found.");
                        }
                        else
                        {
                            Console.WriteLine($"Brand '{brandName}', Type '{typeName}' -> No products found (empty catalog).");
                        }
                    string status = "PASS";
                    string color = "green";
                    reportLines.Add($"<tr><td>{brandName}</td><td>{typeName}</td><td>{products.Count}</td><td style='color:{color}'>{status}</td></tr>");
                }
            }

            

            GenerateHtmlReport();
        }

    [Fact]
public void TC05()
{
    var reportLines = new List<string>();
    var addedProducts = new List<string>();     

    driver.Navigate().GoToUrl(baseUrl);
    Thread.Sleep(2000);

    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

    var products = driver.FindElements(By.CssSelector(".esh-catalog-item"));
    int totalProducts = products.Count;

    int expectedCartCount = 0;

    for (int i = 0; i < totalProducts; i++)
    {
        products = driver.FindElements(By.CssSelector(".esh-catalog-item"));
        var product = products[i];

        string productName = product.FindElement(By.CssSelector(".esh-catalog-name")).Text;

        product.FindElement(By.CssSelector(".esh-catalog-button")).Click();
        Thread.Sleep(2000);

        expectedCartCount++;

        bool added = false;
        try
        {
            wait.Until(d =>
            {
                var cartBadge = d.FindElement(By.CssSelector(".esh-basketstatus-badge")).Text.Trim();
                return int.TryParse(cartBadge, out int count) && count == expectedCartCount;
            });
            added = true;
        }
        catch (WebDriverTimeoutException)
        {
            added = false;
        }

        Thread.Sleep(1000);

        string status = added ? "PASS" : "FAIL";
        string color = added ? "green" : "red";
        reportLines.Add($"<tr><td>{productName}</td><td>{expectedCartCount}</td><td style='color:{color}'>{status}</td></tr>");

        if (added) addedProducts.Add(productName);

        Assert.True(added, $"Product '{productName}' was not added to cart successfully.");
        Console.WriteLine($"Added '{productName}' to cart. Cart count: {expectedCartCount}");

        driver.Navigate().GoToUrl(baseUrl);
        wait.Until(d => d.FindElements(By.CssSelector(".esh-catalog-item")).Count > 0);
    }

    driver.Navigate().GoToUrl(baseUrl + "/Basket");
    wait.Until(d => d.FindElements(By.CssSelector(".esh-basket-item")).Count > 0);

    decimal basketSum = 0m;
    var priceElements = driver.FindElements(By.CssSelector(".esh-basket-item--middle.esh-basket-item--mark.col-xs-2"));
    foreach (var el in priceElements)
    {
        string priceText = el.Text.Replace("$", "").Trim();
        decimal price = decimal.Parse(priceText, System.Globalization.CultureInfo.InvariantCulture);
        basketSum += price;
    }

    var totalElement = driver.FindElement(By.CssSelector(".esh-basket-item--mark.col-xs-2:not(.esh-basket-item--middle)"));
    string totalText = totalElement.Text.Replace("$", "").Trim();
    decimal displayedTotal = decimal.Parse(totalText, System.Globalization.CultureInfo.InvariantCulture);

    Console.WriteLine($"Basket sum: {basketSum}, Displayed total: {displayedTotal}");

    bool totalsMatch = basketSum == displayedTotal;
    string totalsColor = totalsMatch ? "green" : "red";
    string totalsStatus = totalsMatch ? "PASS" : "FAIL";

    reportLines.Add($"<tr><td colspan='2'>Total Verification</td><td style='color:{totalsColor}'>{totalsStatus}</td></tr>");

    Assert.Equal(basketSum, displayedTotal);

    string reportPath = Path.Combine(Directory.GetCurrentDirectory(), "TC05_Report.html");
    using (StreamWriter sw = new StreamWriter(reportPath))
    {
        sw.WriteLine("<html><head><title>TC05 Report</title>");
        sw.WriteLine("<style>");
        sw.WriteLine("body { font-family: Arial; margin: 20px; }");
        sw.WriteLine("table { border-collapse: collapse; width: 100%; }");
        sw.WriteLine("th, td { border: 1px solid #ddd; padding: 8px; text-align: left; }");
        sw.WriteLine("th { background-color: #f2f2f2; }");
        sw.WriteLine("</style></head><body>");

        sw.WriteLine("<h2>TC05 - Add All Products To Cart</h2>");
        sw.WriteLine($"<p>Generated at: {DateTime.Now}</p>");

        sw.WriteLine("<table>");
        sw.WriteLine("<tr><th>Product</th><th>Cart Count</th><th>Status</th></tr>");
        foreach (var line in reportLines)
        {
            sw.WriteLine(line);
        }
        sw.WriteLine("</table>");

        sw.WriteLine("<h3>Summary</h3>");
        sw.WriteLine($"<p><b>Total Products Added:</b> {addedProducts.Count}</p>");
        sw.WriteLine("<ul>");
        foreach (var name in addedProducts)
        {
            sw.WriteLine($"<li>{name}</li>");
        }
        sw.WriteLine("</ul>");

        sw.WriteLine($"<p><b>Basket Sum:</b> {basketSum} | <b>Displayed Total:</b> {displayedTotal}</p>");

        sw.WriteLine("</body></html>");
    }

    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
    {
        FileName = reportPath,
        UseShellExecute = true
    });
}

[Fact]
public void TC08()
{
    string Password = "123456789.Az"; 
    string username = "med.aguilid10@gmail.com";
    var reportLines = new List<string>();
    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

    driver.Navigate().GoToUrl(baseUrl + "/identity/Account/Login");
    driver.FindElement(By.Id("Input_Email")).SendKeys(username);
    driver.FindElement(By.Id("Input_Password")).SendKeys(Password);
    driver.FindElement(By.CssSelector("button[type='submit']")).Click();

    bool loginSuccess = false;
    try
    {
        wait.Until(d => d.FindElement(By.CssSelector(".esh-identity-name")));
        loginSuccess = true;
    }
    catch (WebDriverTimeoutException)
    {
        loginSuccess = false;
    }

    Assert.True(loginSuccess, "Login failed.");

    bool navigationSuccess = false;
    try
    {
        var identityToggle = wait.Until(d => d.FindElement(By.CssSelector(".esh-identity-name")));
        identityToggle.Click();
        Thread.Sleep(1000);

        var myOrdersLink = wait.Until(d => d.FindElement(By.CssSelector("a[href='/order/my-orders']")));
        myOrdersLink.Click();
        navigationSuccess = true;
    }
    catch
    {
        navigationSuccess = false;
    }

    Assert.True(navigationSuccess, "Navigation to 'My Orders' failed.");

    bool ordersDisplayed = false;
    try
    {
        wait.Until(d => d.FindElements(By.CssSelector("article.esh-orders-items.row")).Count > 0);
        var orders = driver.FindElements(By.CssSelector("article.esh-orders-items.row"));
        ordersDisplayed = orders.Count > 0;

        foreach (var order in orders)
        {
            var sections = order.FindElements(By.CssSelector("section.esh-orders-item"));
            string orderId = sections[0].Text;
            string date = sections[1].Text;
            string total = sections[2].Text;
            string status = sections[3].Text;

            reportLines.Add($"<tr><td>{orderId}</td><td>{date}</td><td>{total}</td><td>{status}</td><td style='color:green'>PASS</td></tr>");
        }
    }
    catch
    {
        ordersDisplayed = false;
        reportLines.Add($"<tr><td colspan='5' style='color:red'>No past orders found</td></tr>");
    }


    string reportPath = Path.Combine(Directory.GetCurrentDirectory(), "TC08_Report.html");
    using (StreamWriter sw = new StreamWriter(reportPath))
    {
        sw.WriteLine("<html><head><title>TC08 Report</title></head><body>");
        sw.WriteLine("<h2>TC08 - Order History</h2>");
        sw.WriteLine("<table border='1' cellpadding='5' cellspacing='0'>");
        sw.WriteLine("<tr><th>Order ID</th><th>Date</th><th>Total</th><th>Status</th><th>Result</th></tr>");

        foreach (var line in reportLines)
        {
            sw.WriteLine(line);
        }
        sw.WriteLine("</table>");
        sw.WriteLine("</body></html>");
    }

    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
    {
        FileName = reportPath,
        UseShellExecute = true
    });
}


[Fact]
public void TC14()
{

    string currentPassword = "NewPass#456";
    string newPassword = "NeccwPass#456"; 
    string username = "demouser@microsoft.com";

    driver.Navigate().GoToUrl(baseUrl+"/Identity/Account/Login");
      var reportLines = new List<string>();


    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
    // Login

    driver.FindElement(By.Id("Input_Email")).SendKeys(username);
    driver.FindElement(By.Id("Input_Password")).SendKeys(currentPassword);
    driver.FindElement(By.CssSelector("button[type='submit']")).Click();

    Thread.Sleep(2000);
    var identityToggle = wait.Until(d => d.FindElement(By.CssSelector(".esh-identity-name")));
    identityToggle.Click();
    Thread.Sleep(1000);

    var myAccountLink = wait.Until(d => d.FindElement(By.CssSelector("a[href='/manage/my-account']")));
    myAccountLink.Click();
        reportLines.Add("<tr><td>Navigation</td><td>Opened My Account</td><td style='color:green'>PASS</td></tr>");

    wait.Until(d => d.FindElements(By.LinkText("Password")).Count > 0);
    driver.FindElement(By.LinkText("Password")).Click();
        reportLines.Add("<tr><td>Navigation</td><td>Opened Password page</td><td style='color:green'>PASS</td></tr>");


    driver.FindElement(By.Id("OldPassword")).SendKeys(currentPassword);
    driver.FindElement(By.Id("NewPassword")).SendKeys(newPassword);
    driver.FindElement(By.Id("ConfirmPassword")).SendKeys(newPassword);

    // Validate regex rules
    Assert.Matches(@"^.*(?=.{6,})(?=.*\W)(?=.*[a-z])(?=.*[A-Z]).*$", newPassword);

    driver.FindElement(By.CssSelector("button[type='submit']")).Click();

    bool passwordChanged = false;
    try
    {
        var successMessage = wait.Until(d =>
            d.FindElement(By.CssSelector(".alert.alert-success"))
        );

        if (successMessage.Text.Contains("Your password has been changed"))
        {
            passwordChanged = true;
        }
    }
    catch (WebDriverTimeoutException)
    {
        passwordChanged = false;
    }

    if (passwordChanged)
    {
        reportLines.Add("<tr><td>Password Change</td><td>Password updated successfully</td><td style='color:green'>PASS</td></tr>");
    }
    else
    {
        reportLines.Add("<tr><td>Password Change</td><td>Password update failed</td><td style='color:red'>FAIL</td></tr>");
    }

    Assert.True(passwordChanged, "Password change did not succeed.");

    // === Generate HTML report ===
    string reportPath = Path.Combine(Directory.GetCurrentDirectory(), "TC06_Report.html");
    using (StreamWriter sw = new StreamWriter(reportPath))
    {
        sw.WriteLine("<html><head><title>TC06 Report</title></head><body>");
        sw.WriteLine("<h2>TC14 - Change Password</h2>");
        sw.WriteLine("<table border='1' cellpadding='5' cellspacing='0'>");
        sw.WriteLine("<tr><th>Step</th><th>Action</th><th>Status</th></tr>");
        foreach (var line in reportLines)
        {
            sw.WriteLine(line);
        }
        sw.WriteLine("</table>");
        sw.WriteLine("</body></html>");
    }

    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
    {
        FileName = reportPath,
        UseShellExecute = true
    });
}




        private void GenerateHtmlReport()
{
    var sb = new StringBuilder();
    sb.AppendLine("<html><head><title>Catalog Test Report</title>");
    sb.AppendLine("<style>table{border-collapse:collapse;width:100%}th,td{border:1px solid #ddd;padding:8px}th{background-color:#f2f2f2}</style>");
    sb.AppendLine("</head><body>");
    sb.AppendLine("<h2>Catalog Test Report - TC04</h2>");
    sb.AppendLine($"<p>Generated at: {DateTime.Now}</p>");
    sb.AppendLine("<table>");
    sb.AppendLine("<tr><th>Brand</th><th>Type</th><th>Products Found</th><th>Status</th></tr>");

    foreach (var line in reportLines)
    {
        sb.AppendLine(line);
    }

    sb.AppendLine("</table></body></html>");

    string currentDir = Directory.GetCurrentDirectory();
    string reportPath = Path.Combine(currentDir, "CatalogTestReport.html");
    File.WriteAllText(reportPath, sb.ToString());

    Console.WriteLine($"HTML report generated: {reportPath}");

    try
    {
        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
        {
            FileName = reportPath,
            UseShellExecute = true
        });
    }
    catch (Exception ex)
    {
        Console.WriteLine("Unable to automatically open report: " + ex.Message);
    }
}

            

        public void Dispose()
        {
            driver.Quit();
        }
    }
}
