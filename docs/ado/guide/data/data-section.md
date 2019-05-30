---
title: "Data Section | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "data section [ADO]"
ms.assetid: 43dc42a8-7057-48e6-93d6-880d5c5c51a4
author: MightyPen
ms.author: genemi
manager: craigg
---
# Data Section
The data section defines the data of the rowset along with any pending updates, insertions, or deletions. The data section can contain zero or more rows. It can only contain data from one rowset where the row is defined by the schema. Also, as noted before, columns without any data can be omitted. If an attribute or subelement is used in the data section and that construct has not been defined in the schema section, it is silently ignored.  
  
## String  
 Reserved XML characters in text data must be replaced with appropriate character entities. For example, in the company name "Joe's Garage," the single quotation mark must be replaced by an entity. The actual row would resemble the following:  
  
```  
<z:row CompanyName="Joe's Garage"/>  
```  
  
 The following characters are reserved in XML and must be replaced by character entities: {',",&,\<,>}.  
  
## Binary  
 Binary data is bin.hex encoded (that is, one byte maps to two characters, one character per nibble).  
  
## DateTime  
 The variant VT_DATE format is not directly supported by XML-Data data types. The correct format for dates with both a data and time component is yyyy-mm-ddThh:mm:ss.  
  
 For more information about date formats specified by XML, see the [W3C XML-Data specification](https://go.microsoft.com/fwlink/?LinkId=5692).  
  
 When the XML-Data specification defines two equivalent data types (for example, i4 == int), ADO will write out the friendly name but read in both.  
  
## Managing Pending Changes  
 A Recordset can be opened in immediate or batch update mode. When they are opened in batch update mode with client-side cursors, all changes made to the Recordset are in a pending state until the UpdateBatch method is called. Pending changes are also persisted when the Recordset is saved. In XML, they are represented by the use of the "update" elements defined in urn:schemas-microsoft-com:rowset. In addition, if a rowset can be updated, the updatable property must be set to true in the definition of the row. For example, to define that the Shippers table contains pending changes, the row definition would look resemble following.  
  
```  
<s:ElementType name="row" content="eltOnly" updatable="true">  
  <s:attribute type="ShipperID"/>  
  <s:attribute type="CompanyName"/>  
  <s:attribute type="Phone"/>  
  <s:extends type="rs:rowbase"/>  
</s:ElementType>  
```  
  
 This tells the Persistence Provider to surface data so that ADO can construct an updatable Recordset object.  
  
 The following sample data shows how insertions, changes, and deletions look in the persisted file.  
  
```  
<rs:data>  
  <z:row ShipperID="2" CompanyName="United Package"   
    Phone="(503) 555-3199"/>  
<rs:update>  
  <rs:original>  
    <z:row ShipperID="3" CompanyName="Federal Shipping"   
      Phone="(503) 555-9931"/>  
  </rs:original>  
  <z:row Phone="(503) 552-7134"/>  
</rs:update>  
<rs:insert>  
  <z:row ShipperID="12" CompanyName="Lightning Shipping"   
    Phone="(505) 111-2222"/>  
  <z:row ShipperID="13" CompanyName="Thunder Overnight"   
    Phone="(505) 111-2222"/>  
  <z:row ShipperID="14" CompanyName="Blue Angel Air Delivery"   
    Phone="(505) 111-2222"/>  
</rs:insert>  
<rs:delete>  
  <z:row ShipperID="1" CompanyName="Speedy Express" Phone="(503) 555-9831"/>  
</rs:delete>  
</rs:data>  
```  
  
 An update always contains the entire original row data followed by the changed row data. The changed row may contain all the columns or only those columns that have actually changed. In the previous example, the row for Shipper 2 is not changed, and only the Phone column has changed values for Shipper 3 and is therefore the only column included in the changed row. The inserted rows for Shippers 12, 13, and 14 are batched together under one rs:insert tag. Note that deleted rows can also be batched together, although this is not shown in the previous example.  
  
## See Also  
 [Persisting Records in XML Format](../../../ado/guide/data/persisting-records-in-xml-format.md)
