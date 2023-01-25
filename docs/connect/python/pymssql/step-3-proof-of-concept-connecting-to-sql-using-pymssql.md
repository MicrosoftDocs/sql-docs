---
title: "Step 3: Connecting to SQL using pymssql"
description: "Step 3 is a proof of concept, which shows how you can connect to SQL Server using Python and pymssql. The basic examples demonstrate selecting and inserting data."
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# Step 3: Proof of concept connecting to SQL using pymssql
[!INCLUDE[Driver_Python_Download](../../../includes/driver_python_download.md)]

This example should be considered a proof of concept only.  The sample code is simplified for clarity, and does not necessarily represent best practices recommended by Microsoft.  
  
## Step 1:  Connect  
  
The [pymssql.connect](https://pypi.org/project/pymssql/) function is used to connect to SQL Database.  
  
```python
    import pymssql  
    conn = pymssql.connect(server='yourserver.database.windows.net', user='yourusername@yourserver', password='yourpassword', database='AdventureWorks')  
```  
  
  
## Step 2:  Execute query  
  
The [cursor.execute](https://pypi.org/project/pymssql/) function can be used to retrieve a result set from a query against SQL Database. This function essentially accepts any query and returns a result set, which can be iterated over with the use of [cursor.fetchone()](https://pypi.org/project/pymssql/).  
  
  
```python
    import pymssql  
    conn = pymssql.connect(server='yourserver.database.windows.net', user='yourusername@yourserver', password='yourpassword', database='AdventureWorks')  
    cursor = conn.cursor()  
    cursor.execute('SELECT c.CustomerID, c.CompanyName,COUNT(soh.SalesOrderID) AS OrderCount FROM SalesLT.Customer AS c LEFT OUTER JOIN SalesLT.SalesOrderHeader AS soh ON c.CustomerID = soh.CustomerID GROUP BY c.CustomerID, c.CompanyName ORDER BY OrderCount DESC;')  
    row = cursor.fetchone()  
    while row:  
        print(str(row[0]) + " " + str(row[1]) + " " + str(row[2]))     
        row = cursor.fetchone()  
```  
  
## Step 3:  Insert a row  
  
In this example you will see how to execute an [INSERT](../../../t-sql/statements/insert-transact-sql.md) statement safely and pass parameters. Passing parameters as values protects your application from [SQL injection](../../../relational-databases/tables/primary-and-foreign-key-constraints.md).  
  
  
```python
    import pymssql  
    conn = pymssql.connect(server='yourserver.database.windows.net', user='yourusername@yourserver', password='yourpassword', database='AdventureWorks')  
    cursor = conn.cursor()  
    cursor.execute("INSERT SalesLT.Product (Name, ProductNumber, StandardCost, ListPrice, SellStartDate) OUTPUT INSERTED.ProductID VALUES ('SQL Server Express', 'SQLEXPRESS', 0, 0, CURRENT_TIMESTAMP)")  
    row = cursor.fetchone()  
    while row:  
        print("Inserted Product ID : " +str(row[0]))
        row = cursor.fetchone()  
    conn.commit()
    conn.close()
```  
  
## Step 4: Roll back a transaction  
  
This code example demonstrates the use of transactions in which you:  
  
* Begin a transaction  
* Insert a row of data  
* Roll back your transaction to undo the insert  
  
```python
    import pymssql  
    conn = pymssql.connect(server='yourserver.database.windows.net', user='yourusername@yourserver', password='yourpassword', database='AdventureWorks')  
    cursor = conn.cursor()  
    cursor.execute("BEGIN TRANSACTION")  
    cursor.execute("INSERT SalesLT.Product (Name, ProductNumber, StandardCost, ListPrice, SellStartDate) OUTPUT INSERTED.ProductID VALUES ('SQL Server Express New', 'SQLEXPRESS New', 0, 0, CURRENT_TIMESTAMP)")  
    conn.rollback()  
    conn.close()
```  
    
  ## Next steps  
  
For more information, see the [Python Developer Center](https://azure.microsoft.com/develop/python/).
