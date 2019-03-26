---
title: "Step 3: Proof of concept connecting to SQL using Ruby | Microsoft Docs"
ms.custom: ""
ms.date: "08/08/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: cac20b18-0a6d-4243-bbda-a5d1b9476441
author: MightyPen
ms.author: genemi
manager: craigg
---
# Step 3: Proof of concept connecting to SQL using Ruby

This example should be considered a proof of concept only.  The sample code is simplified for clarity, and does not necessarily represent best practices recommended by Microsoft.  
  
## Step 1:  Connect  
  
The [TinyTDS::Client](https://github.com/rails-sqlserver/tiny_tds) function is used to connect to SQL Database.  
  
``` ruby
    require 'tiny_tds'  
    client = TinyTds::Client.new username: 'yourusername@yourserver', password: 'yourpassword',  
    host: 'yourserver.database.windows.net', port: 1433,  
    database: 'AdventureWorks', azure:true  
```  
  
## Step 2:  Execute a query  
  
Copy and paste the following code in an empty file. Call it test.rb. Then execute it by entering the following command from your command prompt:  
  
    ruby test.rb  
  
In the code sample, the [TinyTds::Result](https://github.com/rails-sqlserver/tiny_tds) function is used to retrieve a result set from a query against SQL Database. This function accepts a query and returns a result set. The results set is iterated over by using [result.each do |row|](https://github.com/rails-sqlserver/tiny_tds).  
  
``` ruby 
    require 'tiny_tds'    
    print 'test'       
    client = TinyTds::Client.new username: 'yourusername@yourserver', password: 'yourpassword',  
    host: 'yourserver.database.windows.net', port: 1433,  
    database: 'AdventureWorks', azure:true  
    results = client.execute("SELECT c.CustomerID, c.CompanyName,COUNT(soh.SalesOrderID) AS OrderCount FROM SalesLT.Customer AS c LEFT OUTER JOIN SalesLT.SalesOrderHeader AS soh ON c.CustomerID = soh.CustomerID GROUP BY c.CustomerID, c.CompanyName ORDER BY OrderCount DESC")  
    results.each do |row|  
    puts row  
    end  
```  
  
## Step 3:  Insert a row  
  
In this example you will see how to execute an [INSERT](../../t-sql/statements/insert-transact-sql.md) statement safely, pass parameters which protect your application from [SQL injection](../../relational-databases/tables/primary-and-foreign-key-constraints.md) value.    
  
To use TinyTDS with Azure, it is recommended that you execute several `SET` statements to change how the current session handles specific information. Recommended `SET` statements are provided in the code sample. For example, `SET ANSI_NULL_DFLT_ON` will allow new columns created to allow null values even if the nullability status of the column is not explicitly stated.  
  
To align with the Microsoft SQL Server [datetime](../../t-sql/data-types/datetime-transact-sql.md) format, use the [strftime](https://ruby-doc.org/core-2.2.0/Time.html#method-i-strftime) function to cast to the corresponding datetime format.  
  
``` ruby
    require 'tiny_tds'  
    client = TinyTds::Client.new username: 'yourusername@yourserver', password: 'yourpassword',  
    host: 'yourserver.database.windows.net', port: 1433,  
    database: 'AdventureWorks', azure:true  
    results = client.execute("SET ANSI_NULLS ON")  
    results = client.execute("SET CURSOR_CLOSE_ON_COMMIT OFF")  
    results = client.execute("SET ANSI_NULL_DFLT_ON ON")  
    results = client.execute("SET IMPLICIT_TRANSACTIONS OFF")  
    results = client.execute("SET ANSI_PADDING ON")  
    results = client.execute("SET QUOTED_IDENTIFIER ON")  
    results = client.execute("SET ANSI_WARNINGS ON")  
    results = client.execute("SET CONCAT_NULL_YIELDS_NULL ON")  
    require 'date'  
    t = Time.now  
    curr_date = t.strftime("%Y-%m-%d %H:%M:%S.%L")  
    results = client.execute("INSERT SalesLT.Product (Name, ProductNumber, StandardCost, ListPrice, SellStartDate)  
    OUTPUT INSERTED.ProductID VALUES ('SQL Server Express New', 'SQLEXPRESS New', 0, 0, '#{curr_date}' )")  
    results.each do |row|  
    puts row  
    end  
```
