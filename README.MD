
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

  <h2>GitHub Project Selected</h2>
  <p>
    <strong>Project Name:</strong> EshopOnWeb <br>
    <strong>Repository:</strong> 
    <a href="https://github.com/dotnet-architecture/eShopOnWeb" target="_blank">https://github.com/dotnet-architecture/eShopOnWeb</a>
  </p>

  <h2>Prerequisites</h2>
  <ul>
    <li>Windows 10 or higher</li>
    <li><a href="https://dotnet.microsoft.com/en-us/download/dotnet/8.0" target="_blank">.NET 8.0 SDK</a></li>
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
    <li><strong>Run EshopOnWeb locally</strong>
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

  <h3>Option 1: Using Visual Studio</h3>
  <ol>
    <li>Open the <strong>Test Explorer</strong> in Visual Studio.</li>
    <li>Build the test project.</li>
    <li>Run all tests (<code>TC04</code>, <code>TC05</code>, <code>TC08</code>, <code>TC14</code>).</li>
  </ol>

  <h3>Option 2: Using .NET CLI</h3>
  <p>You can also run the tests directly from the terminal:</p>

  <ul>
    <li><strong>Run all tests in the project:</strong>
      <pre><code>dotnet test</code></pre>
    </li>
   
    <li><strong>Run an individual test case:</strong>
      <pre><code>dotnet test --filter "FullyQualifiedName~EshopOnWebTests.Tests.TC05"</code></pre>
    </li>
  </ul>

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

  <h2>Test Case Documentation</h2>
  <p>
    A full <strong>Excel file</strong> (<code>TestCases.xlsx</code>) is included in the repository.  
    It documents <strong>15 test cases (TC01 – TC15)</strong> with the following details:
  </p>
  <ul>
    <li>ID</li>
    <li>Title</li>
    <li>Preconditions</li>
    <li>Steps</li>
    <li>Expected Result</li>
    <li>Actual Result</li>
    <li>Status (Pass/Fail)</li>
    <li>Comments</li>
    <li>Type (Manual/Automated)</li>
  </ul>
  <p>This documentation ensures coverage of both <strong>user-side flows</strong> (registration, login, shopping, checkout) and <strong>admin-side flows</strong> (login, product management).</p>

  <h2>Bug Reporting</h2>
  <p>
    As part of the assessment, at least <strong>3 bugs were identified</strong> during testing.  
    Bug reports are provided in a separate document (<code>BugReports.docx</code> or <code>BugReports.xlsx</code>) including:
  </p>
  <ul>
    <li>Bug ID</li>
    <li>Title</li>
    <li>Steps to Reproduce</li>
    <li>Expected vs Actual Behavior</li>
    <li>Severity &amp; Priority with justification</li>
    <li>Suggested Fix or Area to Investigate</li>
  </ul>
  <p>The defect reports follow industry standards and can be imported into tools like Jira or Azure DevOps.</p>

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
