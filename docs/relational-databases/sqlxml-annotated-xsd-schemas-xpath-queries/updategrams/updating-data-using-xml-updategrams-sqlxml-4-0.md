---
title: "Updating Data Using XML Updategrams (SQLXML)"
description: Learn how to update existing data using an XML updategram in SQLXML 4.0.
author: MikeRayMSFT
ms.author: mikeray
ms.date: "03/17/2017"
ms.service: sql
ms.subservice: xml
ms.topic: "reference"
ms.custom: "seo-lt-2019"
helpviewer_keywords:
  - "IDREF type attribute [SQLXML]"
  - "before attribute"
  - "<sync> block"
  - "<after> block"
  - "id attribute"
  - "<before> block"
ms.assetid: 90ef8a33-5ae3-4984-8259-608d2f1d727f
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Updating Data Using XML Updategrams (SQLXML 4.0)
[!INCLUDE [SQL Server Azure SQL Database](../../../includes/applies-to-version/sql-asdb.md)]
  When you update existing data, you must specify both the **\<before>** and **\<after>** blocks. The elements specified in the **\<before>** and **\<after>** blocks describe the desired change. The updategram uses the element(s) that are specified in the **\<before>** block to identify the existing record(s) in the database. The corresponding element(s) in the **\<after>** block indicate how the records should look after executing the update operation. From this information, the updategram creates an SQL statement that matches the **\<after>** block. The updategram then uses this statement to update the database.  
  
 This is the updategram format for an update operation:  
  
```  
<ROOT xmlns:updg="urn:schemas-microsoft-com:xml-updategram">  
<updg:sync [mapping-schema="SampleSchema.xml"]  >  
   <updg:before>  
      <ElementName [updg:id="value"] .../>  
      [<ElementName [updg:id="value"] .../> ... ]  
   </updg:before>  
   <updg:after>  
      <ElementName [updg:id="value"] ... />  
      [<ElementName [updg:id="value"] .../> ...]  
   </updg:after>  
</updg:sync>  
</ROOT>  
```  
  
 **\<updg:before>**  
 The elements in the **\<before>** block identify existing records in the database tables.  
  
 **\<updg:after>**  
 The elements in the **\<after>** block describe how the records specified in the **\<before>** block should look after the updates are applied.  
  
 The **mapping-schema** attribute identifies the mapping schema to be used by the updategram. If the updategram specifies a mapping schema, the element and attribute names specified in the **\<before>** and **\<after>** blocks must match the names in the schema. The mapping schema maps these element or attribute names to the database table and column names.  
  
 If an updategram does not specify a schema, the updategam uses default mapping. In default mapping, the **\<ElementName>** specified in the updategram maps to the database table and the child elements or attributes map to the database columns.  
  
 An element in the **\<before>** block must match with only one table row in the database. If the element either matches multiple table rows or does not match any table row, the updategram returns an error and cancels the entire **\<sync>** block.  
  
 An updategram can include multiple **\<sync>** blocks. Each **\<sync>** block is treated as a transaction. Each **\<sync>** block can have multiple **\<before>** and **\<after>** blocks. For example, if you are updating two of the existing records, you could specify two **\<before>** and **\<after>** pairs, one for each record being updated.  
  
## Using the updg:id Attribute  
 When multiple elements are specified in the **\<before>** and **\<after>** blocks, use the **updg:id** attribute to mark rows in the **\<before>** and **\<after>** blocks. The processing logic uses this information to determine what record in the **\<before>** block pairs with what record in the **\<after>** block.  
  
 The **updg:id** attribute is not necessary (although recommended) if either of the following exists:  
  
-   The elements in the specified mapping schema have the **sql:key-fields** attribute defined on them.  
  
-   There is one or more specific value supplied for the key field(s) in the updategram.  
  
 If either is the case, the updategram uses the key columns that are specified in the **sql:key-fields** to pair the elements in the **\<before>** and **\<after>** blocks.  
  
 If the mapping schema does not identify key columns (by using **sql:key-fields**) or if the updategram is updating a key column value, you must specify **updg:id**.  
  
 The records that are identified in the **\<before>** and **\<after>** blocks do not have to be in the same order. The **updg:id** attribute forces the association between the elements that are specified in the **\<before>** and **\<after>** blocks.  
  
 If you specify one element in the **\<before>** block and only one corresponding element in the **\<after>** block, using **updg:id** is not necessary. However, it is recommended that you specify **updg:id** anyway to avoid ambiguity.  
  
## Examples  
 Before you use the updategram examples, note the following:  
  
-   Most of the examples use default mapping (that is, no mapping schema is specified in the updategram). For more examples of updategrams that use mapping schemas, see [Specifying an Annotated Mapping Schema in an Updategram &#40;SQLXML 4.0&#41;](../../../relational-databases/sqlxml-annotated-xsd-schemas-xpath-queries/updategrams/specifying-an-annotated-mapping-schema-in-an-updategram-sqlxml-4-0.md).  
  
-   Most of the examples use the AdventureWorks sample database. All the updates are applied to the tables in this database. You can restore the AdventureWorks database.  
  
### A. Updating a record  
 The following updategram updates the employee last name to Fuller in the Person.Contact table in the AdventureWorks database. The updategram does not specify any mapping schema; therefore, the updategram uses default mapping.  
  
```  
<ROOT xmlns:updg="urn:schemas-microsoft-com:xml-updategram">  
<updg:sync >  
<updg:before>  
   <Person.Contact ContactID="1" />  
</updg:before>  
<updg:after>  
   <Person.Contact LastName="Abel-Achong" />  
</updg:after>  
</updg:sync>  
</ROOT>  
```  
  
 The record described in the **\<before>** block represents the current record in the database. The updategram uses all of the column values specified in the **\<before>** block to search for the record. In this updategram, the **\<before>** block provides only the ContactID column; therefore, the updategram uses only the value to search for the record. If you were to add the LastName value to this block, the updategram would use both the ContactID and LastName values to search.  
  
 In this updategram, the **\<after>** block provides only the LastName column value because this is the only value that is being changed.  
  
##### To test the updategram  
  
1.  Copy the updategram template above and paste it into a text file. Save the file as UpdateLastName.xml.  
  
2.  Create and use the SQLXML 4.0 Test Script (Sqlxml4test.vbs) to execute the updategram.  

     For more information, see [Using ADO to Execute SQLXML 4.0 Queries](../../../relational-databases/sqlxml/using-ado-to-execute-sqlxml-4-0-queries.md).  
  
### B. Updating multiple records by using the updg:id attribute  
 In this example, the updategram performs two updates on the HumanResources.Shift table in the AdventureWorks database:  
  
-   It changes the name of the original day shift that starts at 7:00AM from "Day" to "Early Morning".  
  
-   It inserts a new shift named "Late Morning" that starts at 10:00AM.  
  
 In the updategram, the **updg:id** attribute creates associations between elements in the **\<before>** and **\<after>** blocks.  
  
```  
<ROOT xmlns:updg="urn:schemas-microsoft-com:xml-updategram">  
  <updg:sync >  
    <updg:before>  
       <HumanResources.Shift updg:id="x" Name="Day" />  
    </updg:before>  
    <updg:after>  
      <HumanResources.Shift updg:id="y" Name="Late Morning"   
                            StartTime="1900-01-01 10:00:00.000"  
                            EndTime="1900-01-01 18:00:00.000"  
                            ModifiedDate="2004-06-01 00:00:00.000"/>  
      <HumanResources.Shift updg:id="x" Name="Early Morning" />  
    </updg:after>  
  </updg:sync>  
</ROOT>  
```  
  
 Notice how the **updg:id** attribute pairs the first instance of the \<HumanResources.Shift> element in the **\<before>** block with the second instance of the \<HumanResources.Shift> element in the **\<after>** block.  
  
##### To test the updategram  
  
1.  Copy the updategram template above and paste it into a text file. Save the file as UpdateMultipleRecords.xml.  
  
2.  Create and use the SQLXML 4.0 Test Script (Sqlxml4test.vbs) to execute the updategram.  
  
     For more information, see [Using ADO to Execute SQLXML 4.0 Queries](../../../relational-databases/sqlxml/using-ado-to-execute-sqlxml-4-0-queries.md).  
  
### C. Specifying multiple \<before> and \<after> blocks  
 To avoid ambiguity, you can write the updategram in Example B by using multiple **\<before>** and **\<after>** block pairs. Specifying **\<before>** and **\<after>** pairs is one way of specifying multiple updates with a minimum of confusion. Also, if each of the **\<before>** and **\<after>** blocks specify at most one element, you do not have to use the **updg:id** attribute.  
  
> [!NOTE]  
>  To form a pair, the **\<after>** tag must immediately follow its corresponding **\<before>** tag.  
  
 In the following updategram, the first **\<before>** and **\<after>** pair updates the shift name for the day shift. The second pair inserts a new shift record.  
  
```  
<ROOT xmlns:updg="urn:schemas-microsoft-com:xml-updategram">  
  <updg:sync >  
    <updg:before>  
       <HumanResources.Shift ShiftID="1" Name="Day" />  
    </updg:before>  
    <updg:after>  
      <HumanResources.Shift Name="Early Morning" />  
    </updg:after>  
    <updg:before>  
    </updg:before>  
    <updg:after>  
      <HumanResources.Shift Name="Late Morning"   
                            StartTime="1900-01-01 10:00:00.000"  
                            EndTime="1900-01-01 18:00:00.000"  
                            ModifiedDate="2004-06-01 00:00:00.000"/>  
    </updg:after>  
  </updg:sync>  
</ROOT>  
```  
  
##### To test the updategram  
  
1.  Copy the updategram template above and paste it into a text file. Save the file as UpdateMultipleBeforeAfter.xml.  
  
2.  Create and use the SQLXML 4.0 Test Script (Sqlxml4test.vbs) to execute the updategram.  
  
     For more information, see [Using ADO to Execute SQLXML 4.0 Queries](../../../relational-databases/sqlxml/using-ado-to-execute-sqlxml-4-0-queries.md).  
  
### D. Specifying multiple \<sync> blocks  
 You can specify multiple **\<sync>** blocks in an updategram. Each **\<sync>** block that is specified is an independent transaction.  
  
 In the following updategram, the first **\<sync>** block updates a record in the Sales.Customer table. For the sake of simplicity, the updategram specifies only the required column values; the identity value (CustomerID) and the value being updated (SalesPersonID).  
  
 The second **\<sync>** block adds two records to the Sales.SalesOrderHeader table. For this table, SalesOrderID is an IDENTITY-type column. Therefore, the updategram does not specify the value of SalesOrderID in each of the \<Sales.SalesOrderHeader> elements.  
  
 Specifying multiple **\<sync>** blocks is useful because if the second **\<sync>** block (a transaction) fails to add records to Sales.SalesOrderHeader table, the first **\<sync>** block can still update the customer record in the Sales.Customer table.  
  
```  
<ROOT xmlns:updg="urn:schemas-microsoft-com:xml-updategram">  
  <updg:sync >  
    <updg:before>  
      <Sales.Customer CustomerID="1" SalesPersonID="280" />  
    </updg:before>  
    <updg:after>  
      <Sales.Customer CustomerID="1" SalesPersonID="283" />  
    </updg:after>  
  </updg:sync>  
  <updg:sync >  
    <updg:before>  
    </updg:before>  
    <updg:after>  
   <Sales.SalesOrderHeader   
             CustomerID="1"  
             RevisionNumber="1"  
             OrderDate="2004-07-01 00:00:00.000"  
             DueDate="2004-07-13 00:00:00.000"  
             OnlineOrderFlag="0"  
             ContactID="378"  
             BillToAddressID="985"  
             ShipToAddressID="985"  
             ShipMethodID="5"  
             SubTotal="24643.9362"  
             TaxAmt="1971.5149"  
             Freight="616.0984"  
             rowguid="01010101-2222-3333-4444-556677889900"  
             ModifiedDate="2004-07-08 00:00:00.000" />  
   <Sales.SalesOrderHeader  
             CustomerID="1"  
             RevisionNumber="1"  
             OrderDate="2004-07-01 00:00:00.000"  
             DueDate="2004-07-13 00:00:00.000"  
             OnlineOrderFlag="0"  
             ContactID="378"  
             BillToAddressID="985"  
             ShipToAddressID="985"  
             ShipMethodID="5"  
             SubTotal="1000.0000"  
             TaxAmt="0.0000"  
             Freight="0.0000"  
             rowguid="10101010-2222-3333-4444-556677889900"  
             ModifiedDate="2004-07-09 00:00:00.000" />  
    </updg:after>  
  </updg:sync>  
</ROOT>  
```  
  
##### To test the updategram  
  
1.  Copy the updategram template above and paste it into a text file. Save the file as UpdateMultipleSyncs.xml.  
  
2.  Create and use the SQLXML 4.0 Test Script (Sqlxml4test.vbs) to execute the updategram.  
  
     For more information, see [Using ADO to Execute SQLXML 4.0 Queries](../../../relational-databases/sqlxml/using-ado-to-execute-sqlxml-4-0-queries.md).  
  
### E. Using a mapping schema  
 In this example, the updategram specifies a mapping schema by using the **mapping-schema** attribute. (There is no default mapping; that is, the mapping schema provides the necessary mapping of elements and attributes in the updategram to the database tables and columns.)  
  
 The elements and attributes specified in the updategram refer to the elements and attributes in the mapping schema.  
  
 The following XSD mapping schema has **\<Customer>**, **\<Order>**, and **\<OD>** elements that map to the Sales.Customer, Sales.SalesOrderHeader, and Sales.SalesOrderDetail tables in the database.  
  
```  
<xsd:schema xmlns:xsd="http://www.w3.org/2001/XMLSchema"  
            xmlns:sql="urn:schemas-microsoft-com:mapping-schema">  
<xsd:annotation>  
  <xsd:appinfo>  
    <sql:relationship name="CustomerOrder"  
          parent="Sales.Customer"  
          parent-key="CustomerID"  
          child="Sales.SalesOrderHeader"  
          child-key="CustomerID" />  
  
    <sql:relationship name="OrderOD"  
          parent="Sales.SalesOrderHeader"  
          parent-key="SalesOrderID"  
          child="Sales.SalesOrderDetail"  
          child-key="SalesOrderID" />  
  </xsd:appinfo>  
</xsd:annotation>  
  
  <xsd:element name="Customer" sql:relation="Sales.Customer" >  
   <xsd:complexType>  
     <xsd:sequence>  
        <xsd:element name="Order"   
                     sql:relation="Sales.SalesOrderHeader"  
                     sql:relationship="CustomerOrder" >  
           <xsd:complexType>  
              <xsd:sequence>  
                <xsd:element name="OD"   
                             sql:relation="Sales.SalesOrderDetail"  
                             sql:relationship="OrderOD" >  
                 <xsd:complexType>  
                  <xsd:attribute name="SalesOrderID" type="xsd:integer" />  
                  <xsd:attribute name="ProductID" type="xsd:integer" />  
                  <xsd:attribute name="UnitPrice" type="xsd:decimal" />  
                  <xsd:attribute name="OrderQty" type="xsd:integer" />  
                  <xsd:attribute name="UnitPriceDiscount" type="xsd:decimal" />   
                 </xsd:complexType>  
                </xsd:element>  
              </xsd:sequence>  
              <xsd:attribute name="CustomerID" type="xsd:string" />  
              <xsd:attribute name="SalesOrderID" type="xsd:integer" />  
              <xsd:attribute name="OrderDate" type="xsd:date" />  
           </xsd:complexType>  
        </xsd:element>  
      </xsd:sequence>  
      <xsd:attribute name="CustomerID"   type="xsd:string" />   
    </xsd:complexType>  
  </xsd:element>  
</xsd:schema>  
```  
  
 This mapping schema (UpdategramMappingSchema.xml) is specified in the following updategram. The updategram adds an order detail item in the Sales.SalesOrderDetail table for a specific order. The updategram includes nested elements: an **\<OD>** element nested inside an **\<Order>** element. The primary key/foreign key relationship between these two elements is specified in the mapping schema.  
  
```  
<ROOT xmlns:updg="urn:schemas-microsoft-com:xml-updategram">  
  <updg:sync mapping-schema="UpdategramMappingSchema.xml" >  
    <updg:before>  
       <Order SalesOrderID="43659" />  
    </updg:before>  
    <updg:after>  
      <Order SalesOrderID="43659" >  
          <OD ProductID="776" UnitPrice="2329.0000"  
              OrderQty="2" UnitPriceDiscount="0.0" />  
      </Order>  
    </updg:after>  
  </updg:sync>  
</ROOT>  
```  
  
##### To test the updategram  
  
1.  Copy the mapping schema above and paste it into a text file. Save the file as UpdategramMappingSchema.xml.  
  
2.  Copy the updategram template above and paste it into a text file. Save the file as UpdateWithMappingSchema.xml in the same folder as was used to save the mapping schema (UpdategramMappingSchema.xml).  
  
3.  Create and use the SQLXML 4.0 Test Script (Sqlxml4test.vbs) to execute the updategram.  
  
     For more information, see [Using ADO to Execute SQLXML 4.0 Queries](../../../relational-databases/sqlxml/using-ado-to-execute-sqlxml-4-0-queries.md).  
  
 For more examples of updategrams that use mapping schemas, see [Specifying an Annotated Mapping Schema in an Updategram &#40;SQLXML 4.0&#41;](../../../relational-databases/sqlxml-annotated-xsd-schemas-xpath-queries/updategrams/specifying-an-annotated-mapping-schema-in-an-updategram-sqlxml-4-0.md).  
  
### F. Using a mapping schema with IDREFS attributes  
 This example illustrates how updategrams use the IDREFS attributes in the mapping schema to update records in multiple tables. For this example, assume that the database consists of the following tables:  
  
-   Student(StudentID, LastName)  
  
-   Course(CourseID, CourseName)  
  
-   Enrollment(StudentID, CourseID)  
  
 Because a student can enroll in many courses and a course can have many students, the third table, the Enrollment table, is required to represent this M:N relationship.  
  
 The following XSD mapping schema provides an XML view of the tables by using the **\<Student>**, **\<Course>**, and **\<Enrollment>** elements. The **IDREFS** attributes in the mapping schema specify the relationship between these elements. The **StudentIDList** attribute on the **\<Course>** element is an **IDREFS** type attribute that refers to the StudentID column in the Enrollment table. Likewise, the **EnrolledIn** attribute on the **\<Student>** element is an **IDREFS** type attribute that refers to the CourseID column in the Enrollment table.  
  
```  
<xsd:schema xmlns:xsd="http://www.w3.org/2001/XMLSchema"  
            xmlns:sql="urn:schemas-microsoft-com:mapping-schema">  
<xsd:annotation>  
  <xsd:appinfo>  
    <sql:relationship name="StudentEnrollment"  
          parent="Student"  
          parent-key="StudentID"  
          child="Enrollment"  
          child-key="StudentID" />  
  
    <sql:relationship name="CourseEnrollment"  
          parent="Course"  
          parent-key="CourseID"  
          child="Enrollment"  
          child-key="CourseID" />  
  </xsd:appinfo>  
</xsd:annotation>  
  
  <xsd:element name="Course" sql:relation="Course"   
                             sql:key-fields="CourseID" >  
    <xsd:complexType>  
    <xsd:attribute name="CourseID"  type="xsd:string" />   
    <xsd:attribute name="CourseName"   type="xsd:string" />   
    <xsd:attribute name="StudentIDList" sql:relation="Enrollment"  
                 sql:field="StudentID"  
                 sql:relationship="CourseEnrollment"   
                                     type="xsd:IDREFS" />  
  
    </xsd:complexType>  
  </xsd:element>  
  <xsd:element name="Student" sql:relation="Student" >  
    <xsd:complexType>  
    <xsd:attribute name="StudentID"  type="xsd:string" />   
    <xsd:attribute name="LastName"   type="xsd:string" />   
    <xsd:attribute name="EnrolledIn" sql:relation="Enrollment"  
                 sql:field="CourseID"  
                 sql:relationship="StudentEnrollment"   
                                     type="xsd:IDREFS" />  
    </xsd:complexType>  
  </xsd:element>  
</xsd:schema>  
```  
  
 Whenever you specify this schema in an updategram and insert a record in the Course table, the updategram inserts a new course record in the Course table. If you specify one or more new student IDs for the StudentIDList attribute, the updategram also inserts a record in the Enrollment table for the each new student. The updategram ensures that no duplicates are added to the Enrollment table.  
  
##### To test the updategram  
  
1.  Create these tables in the database that is specified in the virtual root:  
  
    ```  
    CREATE TABLE Student(StudentID varchar(10) primary key,   
                         LastName varchar(25))  
    CREATE TABLE Course(CourseID varchar(10) primary key,   
                        CourseName varchar(25))  
    CREATE TABLE Enrollment(StudentID varchar(10)   
                                      references Student(StudentID),  
                           CourseID varchar(10)   
                                      references Course(CourseID))  
    ```  
  
2.  Add this sample data:  
  
    ```  
    INSERT INTO Student VALUES ('S1','Davoli')  
    INSERT INTO Student VALUES ('S2','Fuller')  
  
    INSERT INTO Course VALUES  ('CS101', 'C Programming')  
    INSERT INTO Course VALUES  ('CS102', 'Understanding XML')  
  
    INSERT INTO Enrollment VALUES ('S1', 'CS101')  
    INSERT INTO Enrollment VALUES ('S1', 'CS102')  
    ```  
  
3.  Copy the mapping schema above and paste it into a text file. Save the file as SampleSchema.xml.  
  
4.  Save the updategram (SampleUpdategram) in the same folder used to save the mapping schema in the previous step. (This updategram drops a student with StudentID="1" from the CS102 course.)  
  
    ```  
    <ROOT xmlns:updg="urn:schemas-microsoft-com:xml-updategram">  
      <updg:sync mapping-schema="SampleSchema.xml" >  
        <updg:before>  
            <Student updg:id="x" StudentID="S1" LastName="Davolio"  
                                 EnrolledIn="CS101 CS102" />  
        </updg:before>  
        <updg:after >  
            <Student updg:id="x" StudentID="S1" LastName="Davolio"  
                                 EnrolledIn="CS101" />  
        </updg:after>  
      </updg:sync>  
    </ROOT>  
    ```  
  
5.  Create and use the SQLXML 4.0 Test Script (Sqlxml4test.vbs) to execute the updategram.  
  
     For more information, see [Using ADO to Execute SQLXML 4.0 Queries](../../../relational-databases/sqlxml/using-ado-to-execute-sqlxml-4-0-queries.md).  
  
6.  Save and execute the following updategram as described in the previous steps. The updategram adds the student with StudentID="1" back into the CS102 course by adding a record in the Enrollment table.  
  
    ```  
    <ROOT xmlns:updg="urn:schemas-microsoft-com:xml-updategram">  
      <updg:sync mapping-schema="SampleSchema.xml" >  
        <updg:before>  
            <Student updg:id="x" StudentID="S1" LastName="Davolio"  
                                 EnrolledIn="CS101" />  
        </updg:before>  
        <updg:after >  
            <Student updg:id="x" StudentID="S1" LastName="Davolio"  
                                 EnrolledIn="CS101 CS102" />  
        </updg:after>  
      </updg:sync>  
    </ROOT>  
    ```  
  
7.  Save and execute this next updategram as described in the previous steps. This updategram inserts three new students and enrolls them in the CS101 course. Again, the IDREFS relationship inserts records in the Enrollment table.  
  
    ```  
    <ROOT xmlns:updg="urn:schemas-microsoft-com:xml-updategram">  
      <updg:sync mapping-schema="SampleSchema.xml" >  
        <updg:before>  
           <Course updg:id="y" CourseID="CS101"   
                               CourseName="C Programming" />  
        </updg:before>  
        <updg:after >  
           <Student updg:id="x1" StudentID="S3" LastName="Leverling" />  
           <Student updg:id="x2" StudentID="S4" LastName="Pecock" />  
           <Student updg:id="x3" StudentID="S5" LastName="Buchanan" />  
           <Course updg:id="y" CourseID="CS101"  
                               CourseName="C Programming"  
                               StudentIDList="S3 S4 S5" />  
        </updg:after>  
      </updg:sync>  
    </ROOT>  
    ```  
  
 This is the equivalent XDR schema:  
  
```  
<?xml version="1.0" ?>  
<Schema xmlns="urn:schemas-microsoft-com:xml-data"  
        xmlns:dt="urn:schemas-microsoft-com:datatypes"  
        xmlns:sql="urn:schemas-microsoft-com:xml-sql">  
  <ElementType name="Enrollment" sql:relation="Enrollment" sql:key-fields="StudentID CourseID">  
    <AttributeType name="StudentID" dt:type="id" />  
    <AttributeType name="CourseID" dt:type="id" />  
  
    <attribute type="StudentID" />  
    <attribute type="CourseID" />  
  </ElementType>  
  <ElementType name="Course" sql:relation="Course" sql:key-fields="CourseID">  
    <AttributeType name="CourseID" dt:type="id" />  
    <AttributeType name="CourseName" />  
  
    <attribute type="CourseID" />  
    <attribute type="CourseName" />  
  
    <AttributeType name="StudentIDList" dt:type="idrefs" />  
    <attribute type="StudentIDList" sql:relation="Enrollment" sql:field="StudentID" >  
        <sql:relationship  
                key-relation="Course"  
                key="CourseID"  
                foreign-relation="Enrollment"  
                foreign-key="CourseID" />  
    </attribute>  
  
  </ElementType>  
  <ElementType name="Student" sql:relation="Student">  
    <AttributeType name="StudentID" dt:type="id" />  
     <AttributeType name="LastName" />  
  
    <attribute type="StudentID" />  
    <attribute type="LastName" />  
  
    <AttributeType name="EnrolledIn" dt:type="idrefs" />  
    <attribute type="EnrolledIn" sql:relation="Enrollment" sql:field="CourseID" >  
        <sql:relationship  
                key-relation="Student"  
                key="StudentID"  
                foreign-relation="Enrollment"  
                foreign-key="StudentID" />  
    </attribute>  
  
    <element type="Enrollment" sql:relation="Enrollment" >  
        <sql:relationship key-relation="Student"  
                          key="StudentID"  
                          foreign-relation="Enrollment"  
                          foreign-key="StudentID" />  
    </element>  
  </ElementType>  
  
</Schema>  
```  
  
 For more examples of updategrams that use mapping schemas, see [Specifying an Annotated Mapping Schema in an Updategram &#40;SQLXML 4.0&#41;](../../../relational-databases/sqlxml-annotated-xsd-schemas-xpath-queries/updategrams/specifying-an-annotated-mapping-schema-in-an-updategram-sqlxml-4-0.md).  
  
## See Also  
 [Updategram Security Considerations &#40;SQLXML 4.0&#41;](../../../relational-databases/sqlxml-annotated-xsd-schemas-xpath-queries/security/updategram-security-considerations-sqlxml-4-0.md)  
  
  
