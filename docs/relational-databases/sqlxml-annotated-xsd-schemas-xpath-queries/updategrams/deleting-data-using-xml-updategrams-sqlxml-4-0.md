---
title: "Deleting Data Using XML Updategrams (SQLXML)"
description: Learn about deleting data using an XML updategram in SQLXML 4.0.
author: MikeRayMSFT
ms.author: mikeray
ms.date: "03/17/2017"
ms.service: sql
ms.subservice: xml
ms.topic: "reference"
ms.custom: "seo-lt-2019"
helpviewer_keywords:
  - "<after> block"
  - "updategrams [SQLXML], deleting data"
  - "<before> block"
  - "mapping-schema attribute"
  - "record deletions [SQLXML]"
ms.assetid: 4fb116d7-7652-474a-a567-cb475a20765c
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Deleting Data Using XML Updategrams (SQLXML 4.0)
[!INCLUDE [SQL Server Azure SQL Database](../../../includes/applies-to-version/sql-asdb.md)]
  An updategram indicates a delete operation when a record instance appears in the **\<before>** block with no corresponding records in the **\<after>** block. In this case, the updategram deletes the record in the **\<before>** block from the database.  
  
 This is the updategram format for a delete operation:  
  
```  
<ROOT xmlns:updg="urn:schemas-microsoft-com:xml-updategram">  
  <updg:sync [mapping-schema="SampleSchema.xml"]  >  
   <updg:before>  
       <ElementName />  
      [<ElementName .../>... ]  
   </updg:before>  
    [<updg:after>  
    </updg:after>]  
  </updg:sync>  
</ROOT>  
```  
  
 You can omit the **\<after>** tag if the updategram is performing only a delete operation. If you do not specify the optional **mapping-schema** attribute, the **\<ElementName>** specified in the updategram maps to a database table and the child elements or attributes map to columns in the table.  
  
 If an element specified in the updategram either matches more than one row in the table or does not match any row, the updategram returns an error and cancels the entire **\<sync>** block. Only one record at a time can be deleted by an element in the updategram.  
  
## Examples  
 Examples in this section use default mapping (that is, no mapping schema is specified in the updategram). For more examples of updategrams that use mapping schemas, see [Specifying an Annotated Mapping Schema in an Updategram &#40;SQLXML 4.0&#41;](../../../relational-databases/sqlxml-annotated-xsd-schemas-xpath-queries/updategrams/specifying-an-annotated-mapping-schema-in-an-updategram-sqlxml-4-0.md).  
  
 To create working samples using the following examples, you must meet the requirements specified in [Requirements for Running SQLXML Examples](../../../relational-databases/sqlxml/requirements-for-running-sqlxml-examples.md).  
  
### A. Deleting a record by using an updategram  
 The following updategrams deletes two records from the HumanResources.Shift table.  
  
 In these examples, the updategram does not specify a mapping schema. Therefore, the updategram uses default mapping, in which the element name maps to table name and the attributes or subelements map to columns.  
  
 This first updategram is attribute-centric and identifies two shifts (Day-Evening and Evening-Night) in the **\<before>** block. Because there is no corresponding record in the **\<after>** block, this is a delete operation.  
  
```  
<ROOT xmlns:updg="urn:schemas-microsoft-com:xml-updategram">  
<updg:sync >  
  <updg:before>  
       <HumanResources.Shift ShiftID="4"  
                        Name="Day-Evening"  
                        StartTime="1900-01-01 11:00:00.000"  
                        EndTime="1900-01-01 19:00:00.000"  
                        ModifiedDate="2004-01-01 00:00:00.000" />  
       <HumanResources.Shift ShiftID="5"  
                        Name="Evening-Night"  
                        StartTime="1900-01-01 19:00:00.000"  
                        EndTime="1900-01-01 03:00:00.000"  
                        ModifiedDate="2004-01-01 00:00:00.000" />  
  </updg:before>  
  <updg:after>  
  </updg:after>  
</updg:sync>  
</ROOT>  
```  
  
##### To test the updategram  
  
1.  Complete example B ("Inserting multiple records by using an updategram") in [Inserting Data Using XML Updategrams &#40;SQLXML 4.0&#41;](../../../relational-databases/sqlxml-annotated-xsd-schemas-xpath-queries/updategrams/inserting-data-using-xml-updategrams-sqlxml-4-0.md).  
  
2.  Copy the updategram above to Notepad and save it as Updategram-RemoveShifts.xml in the same folder as was used to complete ("Inserting multiple records by using an updategram") in [Inserting Data Using XML Updategrams &#40;SQLXML 4.0&#41;](../../../relational-databases/sqlxml-annotated-xsd-schemas-xpath-queries/updategrams/inserting-data-using-xml-updategrams-sqlxml-4-0.md).  
  
3.  Create and use the SQLXML 4.0 Test Script (Sqlxml4test.vbs) to execute the updategram.  
  
     For more information, see [Using ADO to Execute SQLXML 4.0 Queries](../../../relational-databases/sqlxml/using-ado-to-execute-sqlxml-4-0-queries.md).  
  
## See Also  
 [Updategram Security Considerations &#40;SQLXML 4.0&#41;](../../../relational-databases/sqlxml-annotated-xsd-schemas-xpath-queries/security/updategram-security-considerations-sqlxml-4-0.md)  
  
  
