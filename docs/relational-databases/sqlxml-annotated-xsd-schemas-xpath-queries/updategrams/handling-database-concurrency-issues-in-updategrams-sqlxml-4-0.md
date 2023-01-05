---
title: "Database concurrency issues in Updategrams (SQLXML)"
description: Learn how to use the Optimistic Concurrency Control mechanism in updategrams (SQLXML 4.0) to handle database concurrency issues.
author: MikeRayMSFT
ms.author: mikeray
ms.date: "03/16/2017"
ms.service: sql
ms.subservice: xml
ms.topic: "reference"
ms.custom: "seo-lt-2019"
helpviewer_keywords:
  - "<before> block"
  - "low concurrency protection"
  - "database concurrency [SQLXML]"
  - "timestamp column [SQLXML]"
  - "updategrams [SQLXML], database concurrency"
  - "high concurrency protection [SQLXML]"
  - "optimistic concurrency control"
  - "concurrency [SQLXML]"
  - "intermediate concurrency protection [SQLXML]"
ms.assetid: d4b908d1-b25b-4ad9-8478-9cd882e8c44e
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Handling Database Concurrency Issues in Updategrams (SQLXML 4.0)
[!INCLUDE [SQL Server Azure SQL Database](../../../includes/applies-to-version/sql-asdb.md)]
  Like other database update mechanisms, updategrams must deal with concurrent updates to data in a multiuser environment. Updategrams use the Optimistic Concurrency Control, which uses comparison of select field data as snapshots to ensure that the data to be updated has not been altered by another user application since it was read from the database. Updategrams include these snapshot values in the **\<before>** block of the updategrams. Before updating the database, the updategram checks the values that are specified in the **\<before>** block against the values currently in the database to ensure that the update is valid.  
  
 The Optimistic Concurrency Control offers three levels of protection in an updategram: low (none), intermediate, and high. You can decide what level of protection you need by specifying the updategram accordingly.  
  
## Lowest Level of Protection  
 This level is a blind update, in which the update is processed without reference to other updates that have been made since the database was last read. In such a case, you specify only the primary key column(s) in the **\<before>** block to identify the record, and you specify the updated information in the **\<after>** block.  
  
 For example, the new contact phone number in the following updategram is correct, regardless of what the phone number was previously. Notice how the **\<before>** block specifies only the primary key column (ContactID).  
  
```  
<ROOT xmlns:updg="urn:schemas-microsoft-com:xml-updategram">  
<updg:sync >  
<updg:before>  
   <Person.Contact ContactID="1" />  
</updg:before>  
<updg:after>  
   <Person.Contact ContactID="1" Phone="111-111-1111" />  
</updg:after>  
</updg:sync>  
</ROOT>  
```  
  
## Intermediate Level of Protection  
 In this level of protection, the updategram compares the current value(s) of the data being updated with the value(s) in the database column(s) to ensure that the values have not been changed by some other transaction since the record was read by your transaction.  
  
 You can get this level of protection by specifying the primary key column(s) and the column(s) that you are updating in the **\<before>** block.  
  
 For example, this updategram changes the value in the Phone column of the Person.Contact table for the contact with ContactID of 1. The **\<before>** block specifies the **Phone** attribute to ensure that this attribute value matches the value in the corresponding column in the database before applying the updated value.  
  
```  
<ROOT xmlns:updg="urn:schemas-microsoft-com:xml-updategram">  
<updg:sync >  
<updg:before>  
   <Person.Contact ContactID="1" Phone="398-555-0132" />  
</updg:before>  
<updg:after>  
   <Person.Contact ContactID="1" Phone="111-111-1111" />  
</updg:after>  
</updg:sync>  
</ROOT>  
```  
  
## High Level of Protection  
 A high level of protection ensures that the record remains the same since your application last read that record (that is, since your application has read the record, it has not been changed by any other transaction).  
  
 There are two ways you can get this high level of protection against concurrent updates:  
  
-   Specify additional columns in the table in the **\<before>** block.  
  
     If you specify additional columns in the **\<before>** block, the updategram compares the values that are specified for these columns with the values that were in the database before applying the update. If any of the record columns has changed since your transaction read the record, the updategram does not perform the update.  
  
     For example, the following updategram updates the shift name, but specifies additional columns (StartTime,EndTime) in the **\<before>** block, thereby requesting a higher level of protection against concurrent updates.  
  
    ```  
    <ROOT xmlns:updg="urn:schemas-microsoft-com:xml-updategram">  
    <updg:sync >  
    <updg:before>  
       <HumanResources.Shift ShiftID="1"   
                 Name="Day"   
                 StartTime="1900-01-01 07:00:00.000"   
                 EndTime="1900-01-01 15:00:00.000" />  
    </updg:before>  
    <updg:after>  
       <HumanResources.Shift Name="Morning" />  
    </updg:after>  
    </updg:sync>  
    </ROOT>  
    ```  
  
     This example specifies the highest level of protection by specifying all column values for the record in the **\<before>** block.  
  
-   Specify the timestamp column (if available) in the **\<before>** block.  
  
     Instead of specifying all the record columns in the **\<before**> block, you can just specify the timestamp column (if the table has one) along with the primary key column(s) in the **\<before>** block. The database updates the timestamp column to a unique value after each update of the record. In this case, the updategram compares the value of the timestamp with the corresponding value in the database. The timestamp value stored in the database is a binary value. Therefore, the timestamp column must be specified in the schema as **dt:type="bin.hex"**, **dt:type="bin.base64"**, or **sql:datatype="timestamp"**. (You can specify either the **xml** data type or the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] data type.)  
  
#### To test the updategram  
  
1.  Create this table in the **tempdb** database:  
  
    ```  
    USE tempdb  
    CREATE TABLE Customer (  
                 CustomerID  varchar(5),  
                 ContactName varchar(20),  
                 LastUpdated timestamp)  
    ```  
  
2.  Add this sample record:  
  
    ```  
    INSERT INTO Customer (CustomerID, ContactName) VALUES   
                         ('C1', 'Andrew Fuller')  
    ```  
  
3.  Copy the following XSD schema and paste it into Notepad. Save it as ConcurrencySampleSchema.xml:  
  
    ```  
    <xsd:schema xmlns:xsd="http://www.w3.org/2001/XMLSchema"  
                xmlns:sql="urn:schemas-microsoft-com:mapping-schema">  
      <xsd:element name="Customer" sql:relation="Customer" >  
       <xsd:complexType>  
            <xsd:attribute name="CustomerID"    
                           sql:field="CustomerID"   
                           type="xsd:string" />   
  
            <xsd:attribute name="ContactName"    
                           sql:field="ContactName"   
                           type="xsd:string" />  
  
            <xsd:attribute name="LastUpdated"   
                           sql:field="LastUpdated"   
                           type="xsd:hexBinary"   
                 sql:datatype="timestamp" />  
  
        </xsd:complexType>  
      </xsd:element>  
    </xsd:schema>  
    ```  
  
4.  Copy the following updategram code into Notepad and save it as ConcurrencySampleTemplate.xml in the same directory where you saved the schema created in the previous step. (Note the timestamp value below for LastUpdated will differ in your example Customer table, so copy the actual value for LastUpdated from the table and paste it into the updategram.)  
  
    ```  
    <ROOT xmlns:updg="urn:schemas-microsoft-com:xml-updategram">  
    <updg:sync mapping-schema="SampleSchema.xml" >  
    <updg:before>  
       <Customer CustomerID="C1"   
                 LastUpdated = "0x00000000000007D1" />  
    </updg:before>  
    <updg:after>  
       <Customer ContactName="Robert King" />  
    </updg:after>  
    </updg:sync>  
    </ROOT>  
    ```  
  
5.  Create and use the SQLXML 4.0 Test Script (Sqlxml4test.vbs) to execute the template.  
  
     For more information, see [Using ADO to Execute SQLXML 4.0 Queries](../../../relational-databases/sqlxml/using-ado-to-execute-sqlxml-4-0-queries.md).  
  
 This is the equivalent XDR schema:  
  
```  
<?xml version="1.0" ?>  
<Schema xmlns="urn:schemas-microsoft-com:xml-data"  
        xmlns:dt="urn:schemas-microsoft-com:datatypes"  
        xmlns:sql="urn:schemas-microsoft-com:xml-sql">  
<ElementType name="Customer" sql:relation="Customer" >  
    <AttributeType name="CustomerID" />  
    <AttributeType name="ContactName" />  
    <AttributeType name="LastUpdated"  dt:type="bin.hex"   
                                       sql:datatype="timestamp" />  
    <attribute type="CustomerID" />  
    <attribute type="ContactName" />  
    <attribute type="LastUpdated" />  
</ElementType>  
</Schema>  
```  
  
## See Also  
 [Updategram Security Considerations &#40;SQLXML 4.0&#41;](../../../relational-databases/sqlxml-annotated-xsd-schemas-xpath-queries/security/updategram-security-considerations-sqlxml-4-0.md)  
  
  
