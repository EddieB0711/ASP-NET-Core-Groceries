# ASP-NET-Core-Groceries

This project is specifically for testing different ideas and currently combinds a few different methodologies (ie RESTful requests vs. a messaging based system).

This is an ASP.NET Core web application currently targetting the .NET 4.6 framework. It uses NServiceBus to communicate with a service to perform requested operations. The result of each operation will eventually be sent back to the web app, picked up through SignalR, and displayed for on the UI. 
