This project is intended to be a demonstration of how to properly follow separation of concerns in a ASP.NET application, particularly Web Forms. A simple website that provides CRUD (Create, Read, Update, Delete) methods for objects is demonstrated. In addition to showing proper SoC, it's also a nice example of all the common data storage systems used by .NET projects.

Current Storage Mechanisms

 - In Memory
 - SQLite
 - Loose JSON files
 - MongoDB
 - RavenDb
 - Loose XML files
 - Oracle
 - MS SQL Server
 - NHibernate


Planned

 - MySQL (planned)
 - Postgres (planned)
 - Redis (partially, poorly implemented)
 - Azure DocumentDB
 
## Getting Started

Clone the repository, open `src/FancyStoreDemo.sln` and run the solution. Navigate to /Products.

By in memory data storage is used. This can be changed by opening `src/FancyStoreDemo.PublicWebSite/web.config` and modifying the `DataStorageType` app setting. A list of value values are in `src/FancyStoreDemo.PublicWebSite/App_Start/NinjectWebCommon.cs`.

## Lessons Learned
In memory storage is the cheapest and fastest, although with the obvious caveat that the data goes away when your app shuts down. But in memory storage is handy for unit testing.

Despite being created specifically for .NET, RavenDB was much harder to get up and running than MongoDB. RavenDB kept throwing exceptions, which appear to be a normal part of operation. It also didn't seem nearly as well documented.

MS SQL Server is the easiest of the large relational databases to set up and get testing with, thanks to the simple automatic instance creation and tight VS integration. That goes a long ways towards keeping developers around. The database I'm most familiar with is Oracle, but I certainly would not want to go through the hassle of installing it.