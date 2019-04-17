---
title: "Hierarchical Recordsets in XML | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "hierarchical Recordsets [ADO], in XML"
ms.assetid: 5d4b11c4-c94f-4910-b99b-5b9abc50d791
author: MightyPen
ms.author: genemi
manager: craigg
---
# Hierarchical Recordsets in XML
ADO allows persistence of hierarchical Recordset objects into XML. With hierarchical Recordset objects, the value of a field in the parent Recordset is another Recordset. Such fields are represented as child elements in the XML stream rather than an attribute.  
  
## Remarks  
 The following example demonstrates this case:  
  
```  
Rs.Open "SHAPE {select stor_id, stor_name, state from stores} APPEND ({select stor_id, ord_num, ord_date, qty from sales} AS rsSales RELATE stor_id TO stor_id)", "Provider=MSDataShape;DSN=pubs;Integrated Security=SSPI;"  
```  
  
 The following is the XML format of the persisted Recordset:  
  
```  
<xml xmlns:s="uuid:BDC6E3F0-6DA3-11d1-A2A3-00AA00C14882"     xmlns:dt="uuid:C2F41010-65B3-11d1-A29F-00AA00C14882"     xmlns:rs="urn:schemas-microsoft-com:rowset"   
    xmlns:z="#RowsetSchema">   
  <s:Schema id="RowsetSchema">   
    <s:ElementType name="row" content="eltOnly" rs:updatable="true">   
      <s:AttributeType name="stor_id" rs:number="1"   
        rs:writeunknown="true">   
        <s:datatype dt:type="string" dt:maxLength="4"   
          rs:fixedlength="true" rs:maybenull="false"/>   
      </s:AttributeType>   
      <s:AttributeType name="stor_name" rs:number="2" rs:nullable="true"   
        rs:writeunknown="true">   
          <s:datatype dt:type="string" dt:maxLength="40"/>   
      </s:AttributeType>   
      <s:AttributeType name="state" rs:number="3" rs:nullable="true"   
        rs:writeunknown="true">   
        <s:datatype dt:type="string" dt:maxLength="2"   
          rs:fixedlength="true"/>   
      </s:AttributeType>   
      <s:ElementType name="rsSales" content="eltOnly"   
        rs:updatable="true" rs:relation="010000000100000000000000">   
        <s:AttributeType name="stor_id" rs:number="1"   
          rs:writeunknown="true">   
          <s:datatype dt:type="string" dt:maxLength="4"   
            rs:fixedlength="true" rs:maybenull="false"/>   
        </s:AttributeType>   
        <s:AttributeType name="ord_num" rs:number="2"   
          rs:writeunknown="true">   
          <s:datatype dt:type="string" dt:maxLength="20"   
            rs:maybenull="false"/>   
        </s:AttributeType>   
        <s:AttributeType name="ord_date" rs:number="3"   
          rs:writeunknown="true">   
            <s:datatype dt:type="dateTime" dt:maxLength="16"   
              rs:scale="3" rs:precision="23" rs:fixedlength="true"   
              rs:maybenull="false"/>   
        </s:AttributeType>   
        <s:AttributeType name="qty" rs:number="4" rs:writeunknown="true">   
          <s:datatype dt:type="i2" dt:maxLength="2" rs:precision="5"   
            rs:fixedlength="true" rs:maybenull="false"/>   
        </s:AttributeType>   
        <s:extends type="rs:rowbase"/>   
      </s:ElementType>   
      <s:extends type="rs:rowbase"/>   
    </s:ElementType>   
  </s:Schema>   
  <rs:data>   
    <z:row stor_id="6380" stor_name="Eric the Read Books" state="WA">   
      <rsSales stor_id="6380" ord_num="6871"   
        ord_date="1994-09-14T00:00:00" qty="5"/>   
      <rsSales stor_id="6380" ord_num="722a"   
        ord_date="1994-09-13T00:00:00" qty="3"/>   
    </z:row>   
    <z:row stor_id="7066" stor_name="Barnum's" state="CA">   
      <rsSales stor_id="7066" ord_num="A2976"   
        ord_date="1993-05-24T00:00:00" qty="50"/>   
      <rsSales stor_id="7066" ord_num="QA7442.3"   
        ord_date="1994-09-13T00:00:00" qty="75"/>   
    </z:row>   
    <z:row stor_id="7067" stor_name="News & Brews" state="CA">   
      <rsSales stor_id="7067" ord_num="D4482"   
        ord_date="1994-09-14T00:00:00" qty="10"/>   
      <rsSales stor_id="7067" ord_num="P2121"   
        ord_date="1992-06-15T00:00:00" qty="40"/>   
      <rsSales stor_id="7067" ord_num="P2121"   
        ord_date="1992-06-15T00:00:00" qty="20"/>   
      <rsSales stor_id="7067" ord_num="P2121"   
        ord_date="1992-06-15T00:00:00" qty="20"/>   
    </z:row>   
  </rs:data>   
</xml>   
```  
  
 The exact order of the columns in the parent Recordset is not obvious when it is persisted in this manner. Any field in the parent may contain a child Recordset. The Persistence Provider persists out all scalar columns first as attributes and then persists out all child Recordset "columns" as child elements of the parent row. The ordinal position of the field in the parent Recordset can be obtained by looking at the schema definition of the Recordset. Every field has an OLE DB property, rs:number, defined in the Recordset schema namespace that contains the ordinal number for that field.  
  
 The names of all fields in the child Recordset are concatenated with the name of the field in the parent Recordset that contains this child. This is to ensure that there are no name collisions in cases where parent and child Recordsets both contain a field that is obtained from two different tables but is named singularly.  
  
 When saving hierarchical Recordsets into XML, you should be aware of the following restrictions in ADO:  
  
-   A hierarchical Recordset with pending updates cannot be persisted into XML.  
  
-   A hierarchical Recordset created with a parameterized shape command cannot be persisted (in either XML or ADTG format).  
  
-   ADO currently saves the relationship between the parent and the child Recordsets as a binary large object (BLOB). XML tags to describe this relationship have not yet been defined in the rowset schema namespace.  
  
-   When a hierarchical Recordset is saved, all child Recordsets are saved along with it. If the current Recordset is a child of another Recordset, its parent is not saved. All child Recordsets that form the subtree of the current Recordset are saved.  
  
 When a hierarchical Recordset is reopened from its XML-persisted format, you must be aware of the following limitations:  
  
-   If the child record contains records for which there are no corresponding parent records, these rows are not written out in the XML representation of the hierarchical Recordset. Thus, these rows will be lost when the Recordset is reopened from its persisted location.  
  
-   If a child record has references to more than one parent record, then on reopening the Recordset, the child Recordset may contain duplicate records. However, these duplicates will only be visible if the user works directly with the underlying child rowset. If a chapter is used to navigate the child Recordset (that is the only way to navigate through ADO), the duplicates are not visible.  
  
## See Also  
 [Persisting Records in XML Format](../../../ado/guide/data/persisting-records-in-xml-format.md)
