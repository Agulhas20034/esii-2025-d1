using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Xunit;
using Assert = Xunit.Assert;


public class ProjectsPageTests : IDisposable
{
    private readonly IWebDriver _driver;
    private readonly WebDriverWait _wait;
    private readonly string _baseUrl = "https://localhost:7185/user/projects"; 

    public ProjectsPageTests()
    {
        var options = new ChromeOptions();
        options.AddArgument("--headless"); 
        _driver = new ChromeDriver(options);
        _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
    }

    [Fact]
    public void Page_Should_Display_MyProjects_Title()
    {
        _driver.Navigate().GoToUrl(_baseUrl);
        _wait.Until(d => d.PageSource.Contains("My Projects"));
        Assert.Contains("My Projects", _driver.PageSource);
    }

    [Fact]
    public void Grid_Should_Display_AtLeastOneProjectRow()
    {
        _driver.Navigate().GoToUrl(_baseUrl);
        _wait.Until(d => d.FindElements(By.CssSelector("table tbody tr")).Count > 0);
        var tableRows = _driver.FindElements(By.CssSelector("table tbody tr"));
        Assert.True(tableRows.Count > 0);
    }
    
    [Fact]
    public void EditButton_Should_OpenEditModal()
    {
        _driver.Navigate().GoToUrl(_baseUrl);
        var editButton = _wait.Until(d => d.FindElement(By.CssSelector("button.btn-warning")));
        editButton.Click();

        _wait.Until(d => d.PageSource.Contains("Edit Project"));
        Assert.Contains("Edit Project", _driver.PageSource);
    }

    [Fact]
    public void RemoveButton_Should_OpenDeleteModal()
    {
        _driver.Navigate().GoToUrl(_baseUrl);
        var removeButton = _wait.Until(driver =>
        {
            var buttons = driver.FindElements(By.CssSelector("button.btn-danger"));
            return buttons.FirstOrDefault(b => b.Displayed && b.Enabled);
        });

        removeButton.Click();

        _wait.Until(d => d.PageSource.Contains("Confirm Project Removal"));
        Assert.Contains("Confirm Project Removal", _driver.PageSource);
    }

    public void Dispose()
    {
        _driver.Quit(); 
    }
}
