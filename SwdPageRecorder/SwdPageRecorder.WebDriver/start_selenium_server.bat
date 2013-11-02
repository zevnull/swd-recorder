@set IE=-Dwebdriver.ie.driver="%cd%\IEDriverServer.exe"
@set CHROME=-Dwebdriver.chrome.driver="%cd%\chromedriver.exe"
@set SELENIUM_SERVER_EXE=selenium-server-standalone-2.37.0.jar

start java %CHROME% %IE% -jar %SELENIUM_SERVER_EXE%