---
title: "Retrieving Data Using the AdomdDataReader | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
applies_to: 
  - "SQL Server 2016 Preview"
helpviewer_keywords: 
  - "retrieving data"
  - "AdomdDataReader object"
  - "data retrieval [ADOMD.NET], AdomdDataReader object"
ms.assetid: 8ed7ea26-b5f8-4852-80fc-75dd62df5b3a
caps.latest.revision: 37
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Retrieving Data Using the AdomdDataReader
  When retrieving analytical data, the <xref:Microsoft.AnalysisServices.AdomdClient.AdomdDataReader> object provides a good balance between overhead and interactivity. The <xref:Microsoft.AnalysisServices.AdomdClient.AdomdDataReader> object retrieves a read-only, forward-only, flattened stream of data from an analytical data source. This unbuffered stream of data enables procedural logic to efficiently process results from an analytical data source sequentially. This makes the <xref:Microsoft.AnalysisServices.AdomdClient.AdomdDataReader> a good choice when retrieving large amounts of data for display purposes because the data is not cached in memory.  
  
 The <xref:Microsoft.AnalysisServices.AdomdClient.AdomdDataReader> can also increase application performance by retrieving data as soon as it is available, instead of waiting for the complete results of the query to be returned. The <xref:Microsoft.AnalysisServices.AdomdClient.AdomdDataReader> also reduces system overhead because, by default, this reader stores only one row at a time in memory.  
  
 The tradeoff for optimized performance is that the <xref:Microsoft.AnalysisServices.AdomdClient.AdomdDataReader> object provides less information about retrieved data than other data retrieval methods. The <xref:Microsoft.AnalysisServices.AdomdClient.AdomdDataReader> object does not support a large object model for representing data or metadata, nor does this object model allow for more complex analytical features like cell writeback. However, the <xref:Microsoft.AnalysisServices.AdomdClient.AdomdDataReader> object does provide a set of strongly typed methods for retrieving cellset data and a method for retrieving cellset metadata in a tabular format. Additionally, <xref:Microsoft.AnalysisServices.AdomdClient.AdomdDataReader> implements the **IDbDataReader** interface to support data binding and for retrieving data using the **SelectCommand** method, from the **System.Data** namespace of the Microsoft .NET Framework Class Library.  
  
## Retrieving Data from the AdomdDataReader  
 To use the <xref:Microsoft.AnalysisServices.AdomdClient.AdomdDataReader> object to retrieve data, you follow these steps:  
  
1.  **Create a new instance of the object.**  
  
     To create a new instance of the <xref:Microsoft.AnalysisServices.AdomdClient.AdomdDataReader> class, you call the <xref:Microsoft.AnalysisServices.AdomdClient.AdomdCommand.Execute%2A> or <xref:Microsoft.AnalysisServices.AdomdClient.AdomdCommand.ExecuteReader%2A> method of the <xref:Microsoft.AnalysisServices.AdomdClient.AdomdCommand> object.  
  
2.  **Retrieve data.**  
  
     As the command runs the query, ADOMD.NET returns the results in the **Resultset** format, a tabular format as described in the XML for Analysis specification, to flatten the data for the <xref:Microsoft.AnalysisServices.AdomdClient.AdomdDataReader> object. A tabular format is unusual when querying analytical data considering the variable dimensionality in such data.  
  
     ADOMD.NET stores these tabular results in the network buffer on the client until you request them by using one of the following methods:  
  
    -   Call the <xref:Microsoft.AnalysisServices.AdomdClient.AdomdDataReader.Read%2A> method of the <xref:Microsoft.AnalysisServices.AdomdClient.AdomdDataReader> object.  
  
         The <xref:Microsoft.AnalysisServices.AdomdClient.AdomdDataReader.Read%2A> method obtains a row from the query results. You can then pass the name, or the ordinal reference, of the column to the <xref:Microsoft.AnalysisServices.AdomdClient.AdomdDataReader.Item> property to access each column of the returned row. For example, the first column in the current row is named, ColumnName. Then, either `reader[0].ToString()` or `reader["ColumnName"].ToString()` will return the contents of the first column in the current row.  
  
    -   Call one of the typed accessor methods.  
  
         The <xref:Microsoft.AnalysisServices.AdomdClient.AdomdDataReader> provides a series of typed accessor methods—methods that let you access column values in their native data types. When you know the underlying data type of a column value, a typed accessor method reduces the amount of type conversion required when retrieving the column value, and thereby, provides the highest performance.  
  
         Some of the typed accessor methods that are available include <xref:Microsoft.AnalysisServices.AdomdClient.AdomdDataReader.GetDateTime%2A>, <xref:Microsoft.AnalysisServices.AdomdClient.AdomdDataReader.GetDouble%2A>, and <xref:Microsoft.AnalysisServices.AdomdClient.AdomdDataReader.GetInt32%2A>. For a complete list of typed accessor methods, see <xref:Microsoft.AnalysisServices.AdomdClient.AdomdDataReader>.  
  
3.  **Close the reader.**  
  
     You should always call the <xref:Microsoft.AnalysisServices.AdomdClient.AdomdDataReader.Close%2A> method when you have finished using the <xref:Microsoft.AnalysisServices.AdomdClient.AdomdDataReader> object. While an instance of an <xref:Microsoft.AnalysisServices.AdomdClient.AdomdDataReader> object is open, the <xref:Microsoft.AnalysisServices.AdomdClient.AdomdConnection> is being used exclusively by that <xref:Microsoft.AnalysisServices.AdomdClient.AdomdDataReader>. You will not be able to run any commands on the instance of the <xref:Microsoft.AnalysisServices.AdomdClient.AdomdConnection>, including creating another <xref:Microsoft.AnalysisServices.AdomdClient.AdomdDataReader> or **System.Xml.XmlReader**, until you close the original <xref:Microsoft.AnalysisServices.AdomdClient.AdomdDataReader>.  
  
### Example of Retrieving Data from the AdomdDataReader  
 The following code example iterates through a <xref:Microsoft.AnalysisServices.AdomdClient.AdomdDataReader> object, and returns the first two values, as strings, from each row.  
  
```vb  
If Reader.HasRows Then  
    Do While objReader.Read()  
        Console.WriteLine(vbTab & "{0}" & vbTab & "{1}", _  
            objReader.GetString(0), objReader.GetString(1))  
    Loop  
Else  
  Console.WriteLine("No rows returned.")  
End If  
  
objReader.Close()  
```  
  
```csharp  
if (objReader.HasRows)  
  while (objReader.Read())  
    Console.WriteLine("\t{0}\t{1}", _  
        objReader.GetString(0), objReader.GetString(1));  
else  
  Console.WriteLine("No rows returned.");  
  
objReader.Close();  
```  
  
## Retrieving Metadata from the AdomdDataReader  
 While an instance of an <xref:Microsoft.AnalysisServices.AdomdClient.AdomdDataReader> object is open, you can retrieve schema information, or metadata, about the current recordset using the <xref:Microsoft.AnalysisServices.AdomdClient.AdomdDataReader.GetSchemaTable%2A> method. <xref:Microsoft.AnalysisServices.AdomdClient.AdomdDataReader.GetSchemaTable%2A>returns a **DataTable** object that is populated with the schema information for the current recordset. The **DataTable** will contain one row for each column of the recordset. Each column of the schema table row maps to a property of the column returned in the cellset, where **ColumnName** is the name of the property and the value of the column is the value of the property.  
  
### Example of Retrieving Metadata from the AdomdDataReader  
 The following code example writes out the schema information for an <xref:Microsoft.AnalysisServices.AdomdClient.AdomdDataReader> object.  
  
```vb  
Dim schemaTable As DataTable = objReader.GetSchemaTable()  
  
Dim objRow As DataRow  
Dim objColumn As DataColumn  
  
For Each objRow In schemaTable.Rows  
  For Each objColumn In schemaTable.Columns  
    Console.WriteLine(objColumn.ColumnName & " = " & objRow(objColumn).ToString())  
  Next  
  Console.WriteLine()  
Next  
DataTable schemaTable = objReader.GetSchemaTable();  
```  
  
```csharp  
foreach (DataRow objRow in schemaTable.Rows)  
{  
  foreach (DataColumn objColumn in schemaTable.Columns)  
    Console.WriteLine(objColumn.ColumnName + " = " + objRow[objColumn]);  
  Console.WriteLine();  
}  
```  
  
## Retrieving Multiple Result Sets  
 Data mining supports the concept of nested tables, which ADOMD.NET exposes as nested rowsets. To retrieve the nested rowset associated with each row, you call the <xref:Microsoft.AnalysisServices.AdomdClient.AdomdDataReader.GetDataReader%2A> method.  
  
## See Also  
 [Retrieving Data from an Analytical Data Source](../../analysis-services/multidimensional-models-adomd-net-client/retrieving-data-from-an-analytical-data-source.md)   
 [Retrieving Data Using the CellSet](../../analysis-services/multidimensional-models-adomd-net-client/retrieving-data-using-the-cellset.md)   
 [Retrieving Data Using the XmlReader](../../analysis-services/multidimensional-models-adomd-net-client/retrieving-data-using-the-xmlreader.md)  
  
  