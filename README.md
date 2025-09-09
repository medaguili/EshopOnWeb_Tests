
<body>

  <h1>EshopOnWeb Automated Tests</h1>

  <h2>Overview</h2>
  <p>
    This repository contains automated tests for the <strong>EshopOnWeb</strong> project, a web-based e-commerce platform. 
    The tests are implemented in <strong>C# using Selenium WebDriver and xUnit</strong>.
  </p>

  <p>The tests cover critical user and admin flows including:</p>
  <ul>
    <li>Browsing products by brand and type</li>
    <li>Adding products to the cart</li>
    <li>Verifying total cart price</li>
    <li>Password changing for users</li>
    <li>Order history display</li>
  </ul>

  <p>These tests help ensure that the main user and admin functionalities work correctly.</p>

  <h2>GitHub Project Selected</h2>
  <p>
    <strong>Project Name:</strong> EshopOnWeb <br>
    <strong>Repository:</strong> 
    <a href="https://github.com/dotnet-architecture/eShopOnWeb" target="_blank">https://github.com/dotnet-architecture/eShopOnWeb</a>
  </p>

  <p><strong>Reason for Selection:</strong></p>
  <ul>
    <li>Contains both frontend and backend components</li>
    <li>Provides realistic e-commerce workflows (product catalog, cart, checkout, user management)</li>
    <li>Suitable for demonstrating QA and automation skills</li>
  </ul>

  <h2>Prerequisites</h2>
  <ul>
    <li>Windows 10 or higher</li>
    <li><a href="https://dotnet.microsoft.com/en-us/download/dotnet/6.0" target="_blank">.NET 6.0 SDK</a></li>
    <li><a href="https://visualstudio.microsoft.com/" target="_blank">Visual Studio 2022</a> or other IDE supporting C#</li>
    <li>Microsoft Edge browser (latest version)</li>
    <li>Git</li>
  </ul>

  <h2>Setup Instructions</h2>

  <ol>
    <li><strong>Clone the repository</strong>
      <pre><code>git clone https://github.com/&lt;your-username&gt;/EshopOnWeb-AutomatedTests.git
cd EshopOnWeb-AutomatedTests</code></pre>
    </li>
 .
    <strong>Run EshopOnWeb locally</strong>
      <ul>
        <li>Clone the main project:</li>
        <pre><code>git clone https://github.com/dotnet-architecture/eShopOnWeb.git</code></pre>
        <li>Open the solution in Visual Studio and build the project.</li>
        <li>Ensure the database is configured as described in the EshopOnWeb documentation.</li>
        <li>Run the application locally (usually on <code>https://localhost:5001</code>).</li>
      </ul>
    </li>
.
    <li><strong>Open the test project</strong>
      <ul>
        <li>Open the <code>EshopOnWebTests</code> project in Visual Studio.</li>
        <li>Restore NuGet packages (Selenium, WebDriverManager, xUnit).</li>
      </ul>
    </li>
.
    <li><strong>Configure test settings</strong>
      <pre><code>private readonly string baseUrl = "https://localhost:5001";</code></pre>
    </li>
  </ol>

  <h2>Running the Automated Tests</h2>
  <ol>
    <li>Open the <strong>Test Explorer</strong> in Visual Studio.</li>
    <li>Build the test project.</li>
    <li>Run all tests (<code>TC04</code>, <code>TC05</code>, <code>TC08</code>, <code>TC14</code>).</li>
  </ol>

  <p><strong>Test Execution Notes:</strong></p>
  <ul>
    <li>Tests are implemented using Selenium WebDriver with Edge browser.</li>
    <li>Reports are generated in HTML format after running each test.</li>
    <li>The browser window will open so you can visually inspect the actions.</li>
  </ul>

  <h2>Test Coverage</h2>
  <table>
    <tr>
      <th>Test Case</th>
      <th>Automated?</th>
      <th>Description</th>
    </tr>
    <tr>
      <td>TC04</td>
      <td>Yes</td>
      <td>Browse all product brands/types</td>
    </tr>
    <tr>
      <td>TC05</td>
      <td>Yes</td>
      <td>Add all products to cart &amp; verify total price</td>
    </tr>
    <tr>
      <td>TC08</td>
      <td>Yes</td>
      <td>Verify order history display</td>
    </tr>
    <tr>
      <td>TC14</td>
      <td>Yes</td>
      <td>Change user password following rules</td>
    </tr>
  </table>

  <h2>Assumptions and Notes</h2>
  <ul>
    <li>Application runs locally on <code>https://localhost:5001</code>.</li>
    <li>User accounts used for tests already exist for login/password tests.</li>
    <li>Payment/checkout flows are not fully automated due to missing payment page in the application.</li>
  </ul>

  <h2>Output</h2>
  <p>Each automated test generates an <strong>HTML report</strong> in the test project directory:</p>
  <ul>
    <li><code>TC04_Report.html</code></li>
    <li><code>TC05_Report.html</code></li>
    <li><code>TC08_Report.html</code></li>
    <li><code>TC14_Report.html</code></li>
  </ul>

  <p>These reports summarize the test results, including <strong>pass/fail status, product names, counts, and total prices</strong>.</p>

</body>
</html>
