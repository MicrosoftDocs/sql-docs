---
title: "Using SQLXML Bulk Load in the .NET Environment | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: xml
ms.topic: "reference"
helpviewer_keywords: 
  - "SQLXML, XML Bulk Load"
  - "XML Bulk Load [SQLXML], .NET environment"
  - ".NET Framework [SQLXML], XML Bulk Load"
  - "bulk load [SQLXML], .NET environment"
ms.assetid: b85df83b-ba56-43bf-bcdf-b2a6fca43276
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Using SQLXML Bulk Load in the .NET Environment
  This topic explains how the XML Bulk Load functionality can be used in the .NET environment. For detailed information about XML Bulk Load, see [Performing Bulk Load of XML Data &#40;SQLXML 4.0&#41;](bulk-load-xml/performing-bulk-load-of-xml-data-sqlxml-4-0.md).  
  
 To use the SQLXML Bulk Load COM object from a managed environment, you need to add a project reference to this object. This generates a managed wrapper interface around the Bulk Load COM object.  
  
> [!NOTE]  
>  Managed XML Bulk load does not work with managed streams and requires a wrapper around native streams. The SQLXML Bulk Load component will not run in a multi-threaded environment ('[MTAThread]' attribute). If you attempt to run the Bulk Load component in a multi-thread environment, you get an InvalidCastException exception with the following additional information: "QueryInterface for interface SQLXMLBULKLOADLib.ISQLXMLBulkLoad failed." The workaround is to make the object that contains the Bulk Load object single-thread accessible (for example, by using the **[STAThread]** attribute as shown in the sample).  
  
 This topic provides a working C# sample application to bulk load XML data in the database. To create a working sample, follow these steps:  
  
1.  Create the following tables:  
  
    ```  
    CREATE TABLE Ord (  
             OrderID     int identity(1,1)  PRIMARY KEY,  
             CustomerID  varchar(5))  
    GO  
    CREATE TABLE Product (  
             ProductID   int identity(1,1) PRIMARY KEY,  
             ProductName varchar(20))  
    GO  
    CREATE TABLE OrderDetail (  
           OrderID     int FOREIGN KEY REFERENCES Ord(OrderID),  
           ProductID   int FOREIGN KEY REFERENCES Product(ProductID),  
                       CONSTRAINT OD_key PRIMARY KEY (OrderID, ProductID))  
    GO  
    ```  
  
2.  Save the following schema in a file (schema.xml):  
  
    ```  
    <xsd:schema xmlns:xsd="http://www.w3.org/2001/XMLSchema"  
                xmlns:sql="urn:schemas-microsoft-com:mapping-schema">  
    <xsd:annotation>  
      <xsd:appinfo>  
        <sql:relationship name="OrderOD"  
              parent="Ord"  
              parent-key="OrderID"  
              child="OrderDetail"  
              child-key="OrderID" />  
  
        <sql:relationship name="ODProduct"  
              parent="OrderDetail"  
              parent-key="ProductID"  
              child="Product"  
              child-key="ProductID"   
              inverse="true"/>  
      </xsd:appinfo>  
    </xsd:annotation>  
  
      <xsd:element name="Order" sql:relation="Ord"   
                                sql:key-fields="OrderID" >  
       <xsd:complexType>  
         <xsd:sequence>  
            <xsd:element name="Product" sql:relation="Product"   
                         sql:key-fields="ProductID"  
                         sql:relationship="OrderOD ODProduct">  
              <xsd:complexType>  
                 <xsd:attribute name="ProductID" type="xsd:int" />  
                 <xsd:attribute name="ProductName" type="xsd:string" />  
              </xsd:complexType>  
            </xsd:element>  
         </xsd:sequence>  
            <xsd:attribute name="OrderID"   type="xsd:integer" />   
            <xsd:attribute name="CustomerID"   type="xsd:string" />  
        </xsd:complexType>  
      </xsd:element>  
    </xsd:schema>  
    ```  
  
3.  Save the following sample XML document in a file (data.xml):  
  
    ```  
    <ROOT>    
      <Order OrderID="11" CustomerID="ALFKI">  
        <Product ProductID="11" ProductName="Chai" />  
        <Product ProductID="22" ProductName="Chang" />  
      </Order>  
      <Order OrderID="22" CustomerID="ANATR">  
         <Product ProductID="33" ProductName="Aniseed Syrup" />  
        <Product ProductID="44" ProductName="Gumbo Mix" />  
      </Order>  
    </ROOT>  
    ```  
  
4.  Start Visual Studio.  
  
5.  Create a C# console application.  
  
6.  From the **Project** menu, select **Add Reference**.  
  
7.  In the **COM** tab, select **Microsoft SQLXML Bulkload 4.0 Type Library** (xblkld4.dll) and click **OK**. You will see the **Interop.SQLXMLBULKLOADLib** assembly created in the project.  
  
8.  Replace the Main() method with the following code. Update the **ConnectionString** property and the file path to the schema and data files.  
  
    ```  
    [STAThread]  
       static void Main(string[] args)  
       {     
             try  
             {  
                SQLXMLBULKLOADLib.SQLXMLBulkLoad4Class objBL = new SQLXMLBULKLOADLib.SQLXMLBulkLoad4Class();  
                objBL.ConnectionString = "Provider=sqloledb;server=server;database=databaseName;integrated security=SSPI";  
                objBL.ErrorLogFile = "error.xml";  
                objBL.KeepIdentity = false;  
                objBL.Execute ("schema.xml","data.xml");  
             }  
             catch(Exception e)  
             {  
             Console.WriteLine(e.ToString());  
             }  
       }  
    ```  
  
9. To load the XML in the table you created, build and run the project.  
  
    > [!NOTE]  
    >  The reference to the Bulk Load component (xblkld4.dll) can also be added using the tlbimp.exe tool, which is available as part of .NET framework. This tool creates a managed wrapper for the native DLL (xblkld4.dll), which can then be used in any .NET project. For example:  
  
    ```  
    c:\>tlbimp xblkld4.dll  
    ```  
  
     This creates the managed wrapper DLL (SQLXMLBULKLOADLib.dll) that you can use in the .NET Framework project. In the .NET Framework, you add project reference to the newly created DLL.  
  
## See Also  
 [Performing Bulk Load of XML Data &#40;SQLXML 4.0&#41;](bulk-load-xml/performing-bulk-load-of-xml-data-sqlxml-4-0.md)  
  
  
