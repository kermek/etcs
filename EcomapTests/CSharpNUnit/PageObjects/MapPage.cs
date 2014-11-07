using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;
using OpenQA.Selenium.Interactions;

namespace CSharpNUnit.PageObjects
{
    public class MapPage
    {
        public static readonly By MAP              = By.Id("map-content");
        public static readonly By NAV_BAR          = By.ClassName("container-fluid");
        public static readonly By ADD_PROBLEM_MENU = By.ClassName("b-addProblem");

        private IWebDriver driver;

        public MapPage(IWebDriver driver) {
            this.driver = driver;
        }

        public void SetView(double latitude, double longitude, int zoom)
        {
            int x;
            int y;
            int mapWidth;
            int mapHeight;
            int addProblemWidth = 0;
            int addProblemHeight = 0;

            IJavaScriptExecutor js = driver as IJavaScriptExecutor;
            js.ExecuteScript("var map = document.getElementById('map-content');" +
                "angular.element(map).scope().$parent.$parent.$parent.geoJson._map.setView([" +
                "arguments[0], arguments[1]], arguments[2]);", latitude, longitude, zoom);

            IWebElement map = driver.FindElement(MAP);
            int navBarHeight = driver.FindElement(NAV_BAR).Size.Height;
            IList<IWebElement> addProblem = driver.FindElements(ADD_PROBLEM_MENU);
            mapWidth = map.Size.Width;
            mapHeight = map.Size.Height;
            if (addProblem.Count > 0)
            {
                addProblemWidth = addProblem[0].Size.Width;
                addProblemHeight = addProblem[0].Size.Height;
            }
            if (mapWidth != addProblemWidth)
            {              // wide screen, addProblem at the left side
                x = (mapWidth - addProblemWidth) / 2;
                y = mapHeight / 2;
            }
            else
            {                                          // tall screen, addProblem at the top
                x = mapWidth / 2;
                y = navBarHeight + 1 + addProblemHeight + 1 + (mapHeight - navBarHeight - addProblemHeight - 2) / 2;
            }
            js.ExecuteScript("var map = document.getElementById('map-content');" +
                             "var oldCenter = angular.element(map).scope().$parent.$parent.$parent.geoJson._map.getCenter();" +
                             "var newCenter = angular.element(map).scope().$parent.$parent.$parent" + 
                                                    ".geoJson._map.containerPointToLatLng([arguments[0], arguments[1]]);" +
                             "angular.element(map).scope().$parent.$parent.$parent.geoJson" +
                                    "._map.setView([arguments[2] + oldCenter['lat'] - newCenter['lat']," +
                                                   "arguments[3] + oldCenter['lng'] - newCenter['lng']], arguments[4]);",
                                       x, y, latitude, longitude, zoom);
        }

        public void ClickAtMapCenter(int offset)
        {
            int x;
            int y;
            int mapWidth;
            int mapHeight;
            int addProblemWidth = 0;
            int addProblemHeight = 0;

            IWebElement map = driver.FindElement(MAP);
            int navBarHeight = driver.FindElement(NAV_BAR).Size.Height;
            IList<IWebElement> addProblem = driver.FindElements(ADD_PROBLEM_MENU);
            mapWidth = map.Size.Width;
            mapHeight = map.Size.Height;
            if (addProblem.Count > 0)
            {
                addProblemWidth = addProblem[0].Size.Width;
                addProblemHeight = addProblem[0].Size.Height;
            }
            if (mapWidth != addProblemWidth)
            {              // wide screen, addProblem at the left side
                x = (mapWidth - addProblemWidth) / 2;
                y = mapHeight / 2;
            }
            else
            {                                          // tall screen, addProblem at the top
                x = mapWidth / 2;
                y = navBarHeight + 1 + addProblemHeight + 1 + (mapHeight - navBarHeight - addProblemHeight - 2) / 2;
            }

            Actions builder = new Actions(driver);
            builder.MoveToElement(map, x, y + offset).ClickAndHold().Release().Build().Perform();
        }

        public void clickAtProblemByCoordinateVisible(double latitude, double longitude)
        {

            SetView(latitude, longitude, 18);
            ClickAtMapCenter(-10);
        }

    }
}
