---
title: "Set recursive depth relationships with sql:max-depth"
description: "Learn how to specify depth when querying tables that have a recursive relationship by using the sql:max-depth annotation in an XQuery."
author: MikeRayMSFT
ms.author: mikeray
ms.date: "03/17/2017"
ms.service: sql
ms.subservice: xml
ms.topic: "reference"
ms.custom: "seo-lt-2019"
helpviewer_keywords:
  - "max-depth annotation"
  - "XPath queries [SQLXML], recursive relationships"
  - "depth in recursive relationships [SQLXML]"
  - "annotated XSD schemas, recursive relationships"
  - "relationships [SQLXML], recursive relationships"
  - "self joins"
  - "recursive relationships [SQLXML]"
  - "recursion [SQLXML]"
ms.assetid: 0ffdd57d-dc30-44d9-a8a0-f21cadedb327
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Specifying Depth in Recursive Relationships by Using sql:max-depth
[!INCLUDE [SQL Server Azure SQL Database](../../includes/applies-to-version/sql-asdb.md)]
  In relational databases, when a table is involved in a relationship with itself, it is called a recursive relationship. For example, in a supervisor-supervisee relationship, a table storing employee records is involved in a relationship with itself. In this case, the employees table plays a role of supervisor on one side of the relationship, and the same table plays a role of supervisee on the other side.  
  
 Mapping schemas can include recursive relationships where an element and its ancestor are of the same type.  
  
## Example A  
 Consider the following table:  
  
```  
Emp (EmployeeID, FirstName, LastName, ReportsTo)  
```  
  
 In this table, the ReportsTo column stores the employee ID of the manager.  
  
 Assume that you want to generate an XML hierarchy of employees in which the manager employee is at the top of the hierarchy, and in which the employees that report to that manager appear in the corresponding hierarchy as shown in the following sample XML fragment. What this fragment shows is the *recursive tree* for employee 1.  
  
```  
<?xml version="1.0" encoding="utf-8" ?>   
<root>  
  <Emp FirstName="Nancy" EmployeeID="1" LastName="Devolio">  
     <Emp FirstName="Andrew" EmployeeID="2" LastName="Fuller" />   
     <Emp FirstName="Janet" EmployeeID="3" LastName="Leverling">  
        <Emp FirstName="Margaret" EmployeeID="4" LastName="Peacock">  
          <Emp FirstName="Steven" EmployeeID="5" LastName="Devolio">  
...  
...  
</root>  
```  
  
 In this fragment, employee 5 reports to employee 4, employee 4 reports to employee 3, and employees 3 and 2 reports to employee 1.  
  
 To produce this result, you can use the following XSD schema and specify an XPath query against it. The schema describes an **\<Emp>** element of type EmployeeType, consisting of an **\<Emp>** child element of the same type, EmployeeType. This is a recursive relationship (the element and its ancestor are of the same type). In addition, the schema uses a **\<sql:relationship>** to describe the parent-child relationship between the supervisor and supervisee. Note that in this **\<sql:relationship>**, Emp is both the parent and the child table.  
  
```  
<xsd:schema xmlns:xsd="http://www.w3.org/2001/XMLSchema"  
            xmlns:dt="urn:schemas-microsoft-com:datatypes"  
            xmlns:sql="urn:schemas-microsoft-com:mapping-schema">  
  <xsd:annotation>  
    <xsd:appinfo>  
      <sql:relationship name="SupervisorSupervisee"  
                                  parent="Emp"  
                                  parent-key="EmployeeID"  
                                  child="Emp"  
                                  child-key="ReportsTo" />  
    </xsd:appinfo>  
  </xsd:annotation>  
  <xsd:element name="Emp" type="EmployeeType"   
                          sql:relation="Emp"   
                          sql:key-fields="EmployeeID"   
                          sql:limit-field="ReportsTo" />  
  <xsd:complexType name="EmployeeType">  
    <xsd:sequence>  
      <xsd:element name="Emp" type="EmployeeType"   
                              sql:relation="Emp"   
                              sql:key-fields="EmployeeID"  
                              sql:relationship="SupervisorSupervisee"  
                              sql:max-depth="6" />  
    </xsd:sequence>   
    <xsd:attribute name="EmployeeID" type="xsd:ID" />  
    <xsd:attribute name="FirstName" type="xsd:string"/>  
    <xsd:attribute name="LastName" type="xsd:string"/>  
  </xsd:complexType>  
</xsd:schema>  
```  
  
 Because the relationship is recursive, you need some way to specify the depth of recursion in the schema. Otherwise, the result will be an endless recursion (employee reporting to employee reporting to employee, and so on). The **sql:max-depth** annotation allows you to specify how deep in the recursion to go. In this particular example, to specify a value for **sql:max-depth**, you must know how deep the management hierarchy goes in the company.  
  
> [!NOTE]  
>  The schema specifies the **sql:limit-field** annotation, but does not specify the **sql:limit-value** annotation. This limits the top node in the resulting hierarchy to only those employees who do not report to anyone. (ReportsTo is NULL.) Specifying **sql:limit-field** and not specifying **sql:limit-value** (which defaults to NULL) annotation accomplishes this. If you want the resulting XML to include every possible reporting tree (the reporting tree for every employee in the table), remove the **sql:limit-field** annotation from the schema.  
  
> [!NOTE]  
>  The following procedure uses the tempdb database.  
  
#### To test a sample XPath query against the schema  
  
1.  Create a sample table called Emp in the tempdb database to which the virtual root points.  
  
    ```  
    USE tempdb  
    CREATE TABLE Emp (  
           EmployeeID int primary key,   
           FirstName  varchar(20),   
           LastName   varchar(20),   
           ReportsTo int)  
    ```  
  
2.  Add this sample data:  
  
    ```  
    INSERT INTO Emp values (1, 'Nancy', 'Devolio',NULL)  
    INSERT INTO Emp values (2, 'Andrew', 'Fuller',1)  
    INSERT INTO Emp values (3, 'Janet', 'Leverling',1)  
    INSERT INTO Emp values (4, 'Margaret', 'Peacock',3)  
    INSERT INTO Emp values (5, 'Steven', 'Devolio',4)  
    INSERT INTO Emp values (6, 'Nancy', 'Buchanan',5)  
    INSERT INTO Emp values (7, 'Michael', 'Suyama',6)  
    ```  
  
3.  Copy the schema code above and paste it into a text file. Save the file as maxDepth.xml.  
  
4.  Copy the following template and paste it into a text file. Save the file as maxDepthT.xml in the same directory where you saved maxDepth.xml. The query in the template returns all the employees in the Emp table.  
  
    ```  
    <ROOT xmlns:sql="urn:schemas-microsoft-com:xml-sql">  
      <sql:xpath-query mapping-schema="maxDepth.xml">  
        /Emp  
      </sql:xpath-query>  
    </ROOT>  
    ```  
  
     The directory path specified for the mapping schema (maxDepth.xml) is relative to the directory where the template is saved. An absolute path also can be specified, for example:  
  
    ```  
    mapping-schema="C:\MyDir\maxDepth.xml"  
    ```  
  
5.  Create and use the SQLXML 4.0 Test Script (Sqlxml4test.vbs) to execute the template. For more information, see [Using ADO to Execute SQLXML 4.0 Queries](../../relational-databases/sqlxml/using-ado-to-execute-sqlxml-4-0-queries.md).  

 This is the result:  
  
```  
<?xml version="1.0" encoding="utf-8" ?>   
<root>  
  <Emp FirstName="Nancy" EmployeeID="1" LastName="Devolio">  
  <Emp FirstName="Andrew" EmployeeID="2" LastName="Fuller" />   
    <Emp FirstName="Janet" EmployeeID="3" LastName="Leverling">  
      <Emp FirstName="Margaret" EmployeeID="4" LastName="Peacock">  
        <Emp FirstName="Steven" EmployeeID="5" LastName="Devolio">  
          <Emp FirstName="Nancy" EmployeeID="6" LastName="Buchanan">  
            <Emp FirstName="Michael" EmployeeID="7" LastName="Suyama" />   
          </Emp>  
        </Emp>  
      </Emp>  
    </Emp>  
  </Emp>  
</root>  
```  
  
> [!NOTE]  
>  To produce different depths of hierarchies in the result, change the value of the **sql:max-depth** annotation in the schema and execute the template again after each change.  
  
 In the previous schema, all the **\<Emp>** elements had exactly the same set of attributes (**EmployeeID**, **FirstName**, and **LastName**). The following schema has been slightly modified to return an additional **ReportsTo** attribute for all the **\<Emp>** elements that report to a manager.  
  
 For example, this XML fragment shows the subordinates of employee 1:  
  
```  
<?xml version="1.0" encoding="utf-8" ?>   
<root>  
<Emp FirstName="Nancy" EmployeeID="1" LastName="Devolio">  
  <Emp FirstName="Andrew" EmployeeID="2"   
       ReportsTo="1" LastName="Fuller" />   
  <Emp FirstName="Janet" EmployeeID="3"   
       ReportsTo="1" LastName="Leverling">  
...  
...  
```  
  
 This is the revised schema:  
  
```  
<xsd:schema xmlns:xsd="http://www.w3.org/2001/XMLSchema"  
            xmlns:dt="urn:schemas-microsoft-com:datatypes"  
            xmlns:sql="urn:schemas-microsoft-com:mapping-schema">  
  <xsd:annotation>  
    <xsd:documentation>  
      Customer-Order-Order Details Schema  
      Copyright 2000 Microsoft. All rights reserved.  
    </xsd:documentation>  
    <xsd:appinfo>  
      <sql:relationship name="SupervisorSupervisee"   
                  parent="Emp"  
                  parent-key="EmployeeID"  
                  child="Emp"  
                  child-key="ReportsTo" />  
    </xsd:appinfo>  
  </xsd:annotation>  
  <xsd:element name="Emp"   
                   type="EmpType"   
                   sql:relation="Emp"   
                   sql:key-fields="EmployeeID"   
                   sql:limit-field="ReportsTo" />  
  <xsd:complexType name="EmpType">  
    <xsd:sequence>  
       <xsd:element name="Emp"   
                    type="EmpType"   
                    sql:relation="Emp"   
                    sql:key-fields="EmployeeID"  
                    sql:relationship="SupervisorSupervisee"  
                    sql:max-depth="6"/>  
    </xsd:sequence>   
    <xsd:attribute name="EmployeeID" type="xsd:int" />  
    <xsd:attribute name="FirstName" type="xsd:string"/>  
    <xsd:attribute name="LastName" type="xsd:string"/>  
    <xsd:attribute name="ReportsTo" type="xsd:int" />  
  </xsd:complexType>  
</xsd:schema>  
```  
  
## sql:max-depth Annotation  
 In a schema consisting of recursive relationships, the depth of recursion must be explicitly specified in the schema. This is required to successfully produce the corresponding FOR XML EXPLICIT query that returns the requested results.  
  
 Use the **sql:max-depth** annotation in the schema to specify the depth of recursion in a recursive relationship that is described in the schema. The value of the **sql:max-depth** annotation is a positive integer (1 to 50) that indicates the number of recursions:  A value of 1 stops the recursion at the element for which the **sql:max-depth** annotation is specified; a value of 2 stops the recursion at the next level from the element at which **sql:max-depth** is specified; and so on.  
  
> [!NOTE]  
>  In the underlying implementation, an XPath query that is specified against a mapping schema is converted to a SELECT ... FOR XML EXPLICIT query. This query requires you to specify a finite depth of recursion. The higher the value that you specify for **sql:max-depth**, the larger the FOR XML EXPLICIT query that is generated. This might slow the retrieval time.  
  
> [!NOTE]  
>  Updategrams and XML Bulk Load ignore the max-depth annotation. This means, recursive updates or insertions will happen regardless of what value you specify for max-depth.  
  
## Specifying sql:max-depth on Complex Elements  
 The **sql:max-depth** annotation can be specified on any complex content element.  
  
### Recursive Elements  
 If **sql:max-depth** is specified on both the parent element and the child element in a recursive relationship, the **sql:max-depth** annotation specified on the parent takes precedence. For example, in the following schema, the **sql:max-depth** annotation is specified on both the parent and the child employee elements. In this case, **sql:max-depth=4**, specified on the **\<Emp>** parent element (playing a role of supervisor), takes precedence. The **sql:max-depth** specified on the child **\<Emp>** element (playing a role of supervisee) is ignored.  
  
#### Example B  
  
```  
<xsd:schema xmlns:xsd="http://www.w3.org/2001/XMLSchema"  
            xmlns:dt="urn:schemas-microsoft-com:datatypes"  
            xmlns:sql="urn:schemas-microsoft-com:mapping-schema">  
  <xsd:annotation>  
    <xsd:appinfo>  
      <sql:relationship name="SupervisorSupervisee"  
                                  parent="Emp"  
                                  parent-key="EmployeeID"  
                                  child="Emp"  
                                  child-key="ReportsTo" />  
    </xsd:appinfo>  
  </xsd:annotation>  
  <xsd:element name="Emp" type="EmployeeType"   
                          sql:relation="Emp"   
                          sql:key-fields="EmployeeID"   
                          sql:limit-field="ReportsTo"   
                          sql:max-depth="3" />  
  <xsd:complexType name="EmployeeType">  
    <xsd:sequence>  
      <xsd:element name="Emp" type="EmployeeType"   
                              sql:relation="Emp"   
                              sql:key-fields="EmployeeID"  
                              sql:relationship="SupervisorSupervisee"  
                              sql:max-depth="2" />  
    </xsd:sequence>   
    <xsd:attribute name="EmployeeID" type="xsd:ID" />  
    <xsd:attribute name="FirstName" type="xsd:string"/>  
    <xsd:attribute name="LastName" type="xsd:string"/>  
  </xsd:complexType>  
</xsd:schema>  
```  
  
 To test this schema, follow the steps provided for Sample A, earlier in this topic.  
  
### Nonrecursive Elements  
 If the **sql:max-depth** annotation is specified on an element in the schema that does not cause any recursion, it is ignored. In the following schema, an **\<Emp>** element consists of a **\<Constant>** child element, which, in turn, has an **\<Emp>** child element.  
  
 In this schema, the **sql:max-depth** annotation specified on the **\<Constant>** element is ignored because there is no recursion between the **\<Emp>** parent and the **\<Constant>** child element. But there is recursion between the **\<Emp>** ancestor and the **\<Emp>** child. The schema specifies the **sql:max-depth** annotation on both. Therefore, the **sql:max-depth** annotation that is specified on the ancestor (**\<Emp>** in the supervisor role) takes precedence.  
  
#### Example C  
  
```  
<xsd:schema xmlns:xsd="http://www.w3.org/2001/XMLSchema"   
xmlns:sql="urn:schemas-microsoft-com:mapping-schema">  
  <xsd:annotation>  
    <xsd:appinfo>  
      <sql:relationship name="SupervisorSupervisee"   
                  parent="Emp"   
                  child="Emp"   
                  parent-key="EmployeeID"   
                  child-key="ReportsTo"/>  
    </xsd:appinfo>  
  </xsd:annotation>  
  <xsd:element name="Emp"   
               sql:relation="Emp"   
               type="EmpType"  
               sql:limit-field="ReportsTo"  
               sql:max-depth="1" />  
    <xsd:complexType name="EmpType" >  
      <xsd:sequence>  
       <xsd:element name="Constant"   
                    sql:is-constant="1"   
                    sql:max-depth="20" >  
         <xsd:complexType >  
           <xsd:sequence>  
            <xsd:element name="Emp"   
                         sql:relation="Emp" type="EmpType"  
                         sql:relationship="SupervisorSupervisee"   
                         sql:max-depth="3" />  
         </xsd:sequence>  
         </xsd:complexType>  
         </xsd:element>  
      </xsd:sequence>  
      <xsd:attribute name="EmployeeID" type="xsd:int" />  
    </xsd:complexType>  
</xsd:schema>  
```  
  
 To test this schema, follow the steps provided for Example A, earlier in this topic.  
  
## Complex Types Derived by Restriction  
 If you have a complex type derivation by **\<restriction>**, elements of the corresponding base complex type cannot specify the **sql:max-depth** annotation. In these cases, the **sql:max-depth** annotation can be added to the element of the derived type.  
  
 On the other hand, if you have a complex type derivation by **\<extension>**, the elements of the corresponding base complex type can specify the **sql:max-depth** annotation.  
  
 For example, the following XSD schema generates an error because the **sql:max-depth** annotation is specified on the base type. This annotation is not supported on a type that is derived by **\<restriction>** from another type. To fix this problem, you must change the schema and specify the **sql:max-depth** annotation on element in the derived type.  
  
#### Example D  
  
```  
<xsd:schema xmlns:xsd="http://www.w3.org/2001/XMLSchema"  
            xmlns:dt="urn:schemas-microsoft-com:datatypes"  
            xmlns:msdata="urn:schemas-microsoft-com:mapping-schema">  
  <xsd:complexType name="CustomerBaseType">   
    <xsd:sequence>  
       <xsd:element name="CID" msdata:field="CustomerID" />  
       <xsd:element name="CompanyName"/>  
       <xsd:element name="Customers" msdata:max-depth="3">  
         <xsd:annotation>  
           <xsd:appinfo>  
             <msdata:relationship  
                     parent="Customers"  
                     parent-key="CustomerID"  
                     child-key="CustomerID"  
                     child="Customers" />  
           </xsd:appinfo>  
         </xsd:annotation>  
       </xsd:element>  
    </xsd:sequence>  
  </xsd:complexType>  
  <xsd:element name="Customers" type="CustomerType"/>  
  <xsd:complexType name="CustomerType">  
    <xsd:complexContent>  
       <xsd:restriction base="CustomerBaseType">  
          <xsd:sequence>  
            <xsd:element name="CID"   
                         type="xsd:string"/>  
            <xsd:element name="CompanyName"   
                         type="xsd:string"  
                         msdata:field="CName" />  
            <xsd:element name="Customers"   
                         type="CustomerType" />  
          </xsd:sequence>  
       </xsd:restriction>  
    </xsd:complexContent>  
  </xsd:complexType>  
</xsd:schema>   
```  
  
 In the schema, **sql:max-depth** is specified on a **CustomerBaseType** complex type. The schema also specifies a **\<Customer>** element of type **CustomerType**, which is derived from **CustomerBaseType**. An XPath query specified on such a schema will generate an error, because **sql:max-depth** is not supported on an element that is defined in a restriction base type.  
  
## Schemas with a Deep Hierarchy  
 You might have a schema that includes a deep hierarchy in which an element contains a child element, which in turn contains another child element, and so on. If the **sql:max-depth** annotation specified in such a schema generates an XML document that includes a hierarchy of more than 500 levels (with top-level element at level 1, its child at level 2, and so on), an error is returned.  
  
  
