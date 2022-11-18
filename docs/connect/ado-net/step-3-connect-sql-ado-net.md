---
title: "Step 3: Proof of concept connecting to SQL using ADO.NET"
description: "Contains C# code examples for connecting to SQL Server, executing a query and inserting a row."
author: David-Engel
ms.author: v-davidengel
ms.reviewer: v-kaywon
ms.date: "08/05/2020"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# Step 3: Proof of concept connecting to SQL using ADO.NET

[!INCLUDE[Driver_ADONET_Download](../../includes/driver_adonet_download.md)]

- Previous article:&nbsp;&nbsp;&nbsp;[Step 2: Create a SQL database for ADO.NET development](step-2-create-sql-database-ado-net-development.md)  
- Next article:&nbsp;&nbsp;&nbsp;[Step 4: Connect resiliently to SQL with ADO.NET](step-4-connect-resiliently-sql-ado-net.md)  

  
This C# code example should be considered a proof of concept only. The sample code is simplified for clarity, and does not necessarily represent best practices recommended by Microsoft.  
  
## Step 1: Connect
  
The method **SqlConnection.Open** is used to connect to your SQL database.  


```csharp
using System;
using QC = Microsoft.Data.SqlClient;
  
namespace ProofOfConcept_SQL_CSharp  
{  
	public class Program  
	{  
		static public void Main()  
		{  
			using (var connection = new QC.SqlConnection(  
				"Server=tcp:YOUR_SERVER_NAME_HERE.database.windows.net,1433;" +
				"Database=AdventureWorksLT;User ID=YOUR_LOGIN_NAME_HERE;" +
				"Password=YOUR_PASSWORD_HERE;Encrypt=True;" +
				"TrustServerCertificate=False;Connection Timeout=30;"  
				))  
			{  
				connection.Open();  
				Console.WriteLine("Connected successfully.");  

				Console.WriteLine("Press any key to finish...");  
				Console.ReadKey(true);  
			}  
		}  
	}  
}  
/**** Actual output:  
Connected successfully.  
Press any key to finish...  
****/  
```  


## Step 2: Execute a query  
  
The method SqlCommand.ExecuteReader:  
  
- Issues the SQL SELECT statement to the SQL system.  
- Returns an instance of SqlDataReader to provide access to the result rows.  
  
  
  
```csharp
using System;
using DT = System.Data;
using QC = Microsoft.Data.SqlClient;
  
namespace ProofOfConcept_SQL_CSharp  
{  
	public class Program  
	{  
		static public void Main()  
		{  
			using (var connection = new QC.SqlConnection(  
				"Server=tcp:YOUR_SERVER_NAME_HERE.database.windows.net,1433;" +
				"Database=AdventureWorksLT;User ID=YOUR_LOGIN_NAME_HERE;" +
				"Password=YOUR_PASSWORD_HERE;Encrypt=True;" +
				"TrustServerCertificate=False;Connection Timeout=30;"  
				))  
			{  
				connection.Open();  
				Console.WriteLine("Connected successfully.");  
  
				Program.SelectRows(connection);  
  
				Console.WriteLine("Press any key to finish...");  
				Console.ReadKey(true);  
			}  
		}  
  
		static public void SelectRows(QC.SqlConnection connection)  
		{  
			using (var command = new QC.SqlCommand())  
			{  
				command.Connection = connection;  
				command.CommandType = DT.CommandType.Text;  
				command.CommandText = @"  
SELECT  
	TOP 5  
		COUNT(soh.SalesOrderID) AS [OrderCount],  
		c.CustomerID,  
		c.CompanyName  
	FROM  
						SalesLT.Customer         AS c  
		LEFT OUTER JOIN SalesLT.SalesOrderHeader AS soh  
			ON c.CustomerID = soh.CustomerID  
	GROUP BY  
		c.CustomerID,  
		c.CompanyName  
	ORDER BY  
		[OrderCount] DESC,  
		c.CompanyName; ";  
  
				QC.SqlDataReader reader = command.ExecuteReader();  
  
				while (reader.Read())  
				{  
					Console.WriteLine("{0}\t{1}\t{2}",  
						reader.GetInt32(0),  
						reader.GetInt32(1),  
						reader.GetString(2));  
				}  
			}  
		}  
	}  
}  
/**** Actual output:  
Connected successfully.  
1       29736   Action Bicycle Specialists  
1       29638   Aerobic Exercise Company  
1       29546   Bulk Discount Store  
1       29741   Central Bicycle Specialists  
1       29612   Channel Outlet  
Press any key to finish...  
****/  
```  
  
  
  
## Step 3: Insert a row  
  
  
This example demonstrates how to:  
  
- Execute an SQL INSERT statement safely by passing parameters.  
  - Use of parameters protects against SQL injection attacks.  
- Retrieve the auto-generated value.  
  
  
  
```csharp
using System;
using DT = System.Data;
using QC = Microsoft.Data.SqlClient;
  
namespace ProofOfConcept_SQL_CSharp  
{  
	public class Program  
	{  
		static public void Main()  
		{  
			using (var connection = new QC.SqlConnection(  
				"Server=tcp:YOUR_SERVER_NAME_HERE.database.windows.net,1433;" +
				"Database=AdventureWorksLT;User ID=YOUR_LOGIN_NAME_HERE;" +
				"Password=YOUR_PASSWORD_HERE;Encrypt=True;" +
				"TrustServerCertificate=False;Connection Timeout=30;"  
				))  
			{  
				connection.Open();  
				Console.WriteLine("Connected successfully.");  
  
				Program.InsertRows(connection);  
  
				Console.WriteLine("Press any key to finish...");  
				Console.ReadKey(true);  
			}  
		}  
  
		static public void InsertRows(QC.SqlConnection connection)  
		{  
			QC.SqlParameter parameter;  
  
			using (var command = new QC.SqlCommand())  
			{  
				command.Connection = connection;  
				command.CommandType = DT.CommandType.Text;  
				command.CommandText = @"  
INSERT INTO SalesLT.Product  
		(Name,  
		ProductNumber,  
		StandardCost,  
		ListPrice,  
		SellStartDate  
		)  
	OUTPUT  
		INSERTED.ProductID  
	VALUES  
		(@Name,  
		@ProductNumber,  
		@StandardCost,  
		@ListPrice,  
		CURRENT_TIMESTAMP  
		); ";  
  
				parameter = new QC.SqlParameter("@Name", DT.SqlDbType.NVarChar, 50);  
				parameter.Value = "SQL Server Express 2014";  
				command.Parameters.Add(parameter);  
  
				parameter = new QC.SqlParameter("@ProductNumber", DT.SqlDbType.NVarChar, 25);  
				parameter.Value = "SQLEXPRESS2014";  
				command.Parameters.Add(parameter);  
  
				parameter = new QC.SqlParameter("@StandardCost", DT.SqlDbType.Int);  
				parameter.Value = 11;  
				command.Parameters.Add(parameter);  
  
				parameter = new QC.SqlParameter("@ListPrice", DT.SqlDbType.Int);  
				parameter.Value = 12;  
				command.Parameters.Add(parameter);  
  
				int productId = (int)command.ExecuteScalar();  
				Console.WriteLine("The generated ProductID = {0}.", productId);  
			}  
		}  
	}  
}  
/**** Actual output:  
Connected successfully.  
The generated ProductID = 1000.  
Press any key to finish...  
****/  
```
