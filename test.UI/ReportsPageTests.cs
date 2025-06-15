using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using Xunit;
using Assert = Xunit.Assert;

public class ReportsPageTests : IDisposable
{
    private readonly IWebDriver _driver;
    private readonly WebDriverWait _wait;

    public ReportsPageTests()
    {
        var service = ChromeDriverService.CreateDefaultService();
        service.SuppressInitialDiagnosticInformation = true;

        var options = new ChromeOptions();
        options.AddArguments("--headless=new");
        options.AddArguments("--ignore-certificate-errors");
        options.AddArguments("--disable-gpu");
        options.AddArguments("--window-size=1920,1080");
        _driver = new ChromeDriver(service, options);
        _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
    }

    [Fact]
    public void Should_Display_NoReportsMessage_When_NoReportsExist()
    {
        
        _driver.Navigate().GoToUrl("https://localhost:7185/user/reports");

        
        var alert = _wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("alert-info")));

        
        Assert.Contains("No reports found", alert.Text);
    }
    
    [Fact]
    public void Should_Display_ReportList_When_ReportsExist()
    {
        
        _driver.Navigate().GoToUrl("https://localhost:7185/user/reports");

        var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        var reportTable = wait.Until(d =>
            d.FindElement(By.CssSelector("table.table-striped")));

        
        Assert.NotNull(reportTable);

        
        var rows = reportTable.FindElements(By.CssSelector("tbody tr"));
        Assert.NotEmpty(rows);
    }
    
    [Fact]
    public void Should_Open_ReportDetailsModal_When_ViewButtonClicked()
    {
        
        _driver.Navigate().GoToUrl("https://localhost:7185/user/reports");

        var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        
        wait.Until(d => d.FindElement(By.CssSelector("table.table-striped")));

        
        var viewButton = _driver.FindElement(By.CssSelector("button.report-row"));
        viewButton.Click();

        var modal = wait.Until(d =>
            d.FindElement(By.Id("report-details-modal")));
        
        Assert.True(modal.Displayed);
    }

    public void Dispose()
    {
        _driver.Quit();
    }
}
