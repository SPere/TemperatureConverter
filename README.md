# TemperatureConverter
Convert between Celsius, Kelvin and Fahrenheit temperature values. Project makes use of Angular 12 and WebAPI (.netcore 5)

To run the website:

1. Open src/TemperatureConverterWebApi/TemperatureConverterWebApi.sln in Visual Studio 2019 and run the code. Please make sure .netcore 5 SDK is installed on your machine. 
i. The api swagger should be available at http://localhost:55555/swagger/index.html or you can get to the endpoint directly like http://localhost:55555/api/TemperatureConverter/12.34/Celsius/Fahrenheit

2. Open command prompt, navigate to src/TemperatureConverterWebUI/ and run the below:
i. npm i : to pull down the node packages, 
ii. npm start : to run ng server and start the server,
iii. browse to http://localhost:4200/
iv. npm run test: to run the tests

Vola!

Few points:

1. There are 2 components in the Angular 12 Application. The User has to click the 'Convert' button in the Simple component to get the result. The Advance component uses RXJs to detect when the user has changed the unit dropdowns or the from value and automatically calls the webapi. I feel the user experience is better with the Advance component.

2. The Convert button is disabled and a spinner is shown whilst the Angular Applicaiton is getting the result from the webAPI endpoint. This is so the user can see that their request is being worked on. Validation is also performed when the user clicks the Convert button.

3. If the webAPI is not available, we show the user a friendly message: "Sorry we encountered an error!" and the actual error message, but the actual error message can be hidden or logged to a monitoring service like Azure Monitor / Azure Insights for investigation and alerting.

4. In both WebAPI and Angular, a TemperatureUnit enum has been used to make it easy to coordinate the from and to unit.

5. For the WebAPI, please make you have .netcore 5 SDK install and the latest version of Visual Studio 2019

6. The temperature percision is set in /Constants/TemperaturePrecision.cs file, the currently value is 5 i.e. 5 decimal places in the webapi
