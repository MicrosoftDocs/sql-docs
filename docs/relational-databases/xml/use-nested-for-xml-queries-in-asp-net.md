---
title: "Use Nested FOR XML Queries in ASP.NET"
description: Learn how to use nested FOR XML queries in an ASP.NET application to generate element-centric XML.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: randolphwest
ms.date: 03/17/2026
ai-usage: ai-assisted
ms.service: sql
ms.subservice: xml
ms.topic: how-to
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "FOR XML clause, nested FOR XML queries"
  - "queries [XML in SQL Server], ASP.NET and"
  - "nested FOR XML queries in ASP.NET"
  - "ASP.NET [SQL Server]"
---
# Use nested FOR XML queries in ASP.NET

[!INCLUDE [SQL Server Azure SQL Database FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

In this example, an ASP.NET application returns XML to a browser by executing a stored procedure in SQL Server. The stored procedure generates XML by using nested queries. A similar SELECT statement is shown in the article [Generating Siblings by Using a Nested AUTO Mode Query](../../relational-databases/xml/generate-siblings-with-a-nested-auto-mode-query.md). This example demonstrates one way to use nested FOR XML queries to generate element-centric XML in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].

## Example

```sql
CREATE PROC GetSalesOrderInfo AS
SELECT
      (SELECT top 2 SalesOrderID, SalesPersonID, CustomerID,
         (select top 3 SalesOrderID, ProductID, OrderQty, UnitPrice
           from Sales.SalesOrderDetail
            WHERE  SalesOrderDetail.SalesOrderID = SalesOrderHeader.SalesOrderID
            FOR XML AUTO, TYPE)
      FROM  Sales.SalesOrderHeader
        WHERE SalesOrderHeader.SalesOrderID = SalesOrder.SalesOrderID
      for xml auto, type),
        (SELECT *
         FROM  (SELECT SalesPersonID, EmployeeID
              FROM Sales.SalesPerson, HumanResources.Employee
              WHERE SalesPerson.SalesPersonID = Employee.EmployeeID) As SalesPerson
         WHERE  SalesPerson.SalesPersonID = SalesOrder.SalesPersonID
       FOR XML AUTO, TYPE, ELEMENTS)
FROM (SELECT SalesOrderHeader.SalesOrderID, SalesOrderHeader.SalesPersonID
      FROM Sales.SalesOrderHeader, Sales.SalesPerson
      WHERE SalesOrderHeader.SalesPersonID = SalesPerson.SalesPersonID
     ) as SalesOrder
ORDER BY SalesOrder.SalesOrderID
FOR XML AUTO, TYPE
GO
```

This is the .aspx application. It executes the stored procedure and returns XML in the browser:

```csharp
<%@LANGUAGE=C# Debug=true %>
<%@import Namespace="System.Xml"%>
<%@import namespace="Microsoft.Data.SqlClient" %><%
Response.Expires = -1;
Response.ContentType = "text/xml";
%>

<%
using(Microsoft.Data.SqlClient.SqlConnection c = new Microsoft.Data.SqlClient.SqlConnection("Data Source=server;Database=AdventureWorks;Integrated Security=SSPI;"))
using(Microsoft.Data.SqlClient.SqlCommand cmd = c.CreateCommand())
{
   cmd.CommandText = "GetSalesOrderInfo";
   cmd.CommandType = CommandType.StoredProcedure;
   cmd.Connection.Open();
   System.Xml.XmlReader r = cmd.ExecuteXmlReader();
   System.Xml.XmlTextWriter w = new System.Xml.XmlTextWriter(Response.Output);
   w.WriteStartElement("Root");
   r.MoveToContent();
   while(! r.EOF)
   {
      w.WriteNode(r, true);
   }
   w.WriteEndElement();
   w.Flush();
}
%>
```

### Test the application

1. Create the stored procedure in the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database.

2. Save the .aspx application in the c:\inetpub\wwwroot directory (GetSalesOrderInfo.aspx).

3. Execute the application (`https://server/GetSalesOrderInfo.aspx`).

## Related content

- [Use nested FOR XML queries](use-nested-for-xml-queries.md)
