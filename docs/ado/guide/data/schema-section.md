---
title: "Schema Section"
description: "Schema Section"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "Schema section [ADO]"
---
# Schema Section
The schema section is required. As the previous example shows, ADO writes out detailed metadata about each column to preserve the semantics of the data values as much as possible for updating. However, to load in the XML, ADO only requires the names of the columns and the rowset to which they belong. Here is an example of a minimal schema:  
  
```  
<xml xmlns:s="uuid:BDC6E3F0-6DA3-11d1-A2A3-00AA00C14882"  
    xmlns:rs="urn:schemas-microsoft-com:rowset"  
    xmlns:z="#RowsetSchema">  
  <s:Schema id="RowsetSchema">  
    <s:ElementType name="row" content="eltOnly">  
      <s:AttributeType name="ShipperID"/>  
      <s:AttributeType name="CompanyName"/>  
      <s:AttributeType name="Phone"/>  
      <s:Extends type="rs:rowbase"/>  
    </s:ElementType>  
  </s:Schema>  
  <rs:data>  
...  
  </rs:data>  
</xml>  
```  
  
 In the previous example, ADO will treat the data as variable length strings because no type information is included in the schema.  
  
## Creating Aliases for Column Names  
 The rs:name attribute allows you to create an alias for a column name so that a friendly name may appear in the column information exposed by the rowset and a shorter name may be used in the data section. For example, the previous schema could be modified to map ShipperID to s1, CompanyName to s2, and Phone to s3 as follows:  
  
```  
<s:Schema id="RowsetSchema">   
<s:ElementType name="row" content="eltOnly" rs:updatable="true">   
<s:AttributeType name="s1" rs:name="ShipperID" rs:number="1" ...>   
...  
</s:AttributeType>   
<s:AttributeType name="s2" rs:name="CompanyName" rs:number="2" ...>   
...  
</s:AttributeType>   
<s:AttributeType name="s3" rs:name="Phone" rs:number="3" ...>   
...  
</s:AttributeType>   
...  
</s:ElementType>   
</s:Schema>  
```  
  
 Then, in the data section, the row would use the name attribute (not rs:name) to refer to that column:  
  
```  
"<row s1="1" s2="Speedy Express" s3="(503) 555-9831"/>  
```  
  
 Creating aliases for column names is required whenever a column name is not a valid attribute or tag name in XML. For example, "LastName" must have an alias because names with embedded spaces are invalid attributes. The following line will not be correctly handled by the XML parser, so you must create an alias to some other name that does not have an embedded space.  
  
```  
<row last name="Jones"/>  
```  
  
 Whatever value you use for the name attribute must be used consistently in each place that the column is referenced in both the schema and data sections of the XML document. The following example shows the consistent use of s1:  
  
```  
<s:Schema id="RowsetSchema">  
  <s:ElementType name="row" content="eltOnly">  
    <s:attribute type="s1"/>  
    <s:attribute type="CompanyName"/>  
    <s:attribute type="s3"/>  
    <s:extends type="rs:rowbase"/>  
  </s:ElementType>  
  <s:AttributeType name="s1" rs:name="ShipperID" rs:number="1"   
    rs:maydefer="true" rs:writeunknown="true">  
    <s:datatype dt:type="i4" dt:maxLength="4" rs:precision="10"   
      rs:fixedlength="true" rs:maybenull="true"/>  
  </s:AttributeType>  
</s:Schema>  
<rs:data>  
  <z:row s1="1" CompanyName="Speedy Express" s3="(503) 555-9831"/>  
</rs:data>  
```  
  
 Similarly, because there is no alias defined for `CompanyName` in the previous example, `CompanyName` must be used consistently throughout the document.  
  
## Data Types  
 You can apply a data type to a column with the dt:type attribute. For the definitive guide to allowed XML types, see the Data Types section of the [W3C XML-Data specification](http://www.w3.org/TR/1998/NOTE-XML-data/). You can specify a data type in two ways: either specify the dt:type attribute directly on the column definition itself or use the s:datatype construct as a nested element of the column definition. For example,  
  
```  
<s:AttributeType name="Phone" >  
  <s:datatype dt:type="string"/>  
</s:AttributeType>  
```  
  
 is equivalent to  
  
```  
<s:AttributeType name="Phone" dt:type="string"/>  
```  
  
 If you omit the dt:type attribute entirely from the row definition, by default, the column's type will be a variable length string.  
  
 If you have more type information than simply the type name (for example, dt:maxLength), it makes it more readable to use the s:datatype child element. This is merely a convention, however, and not a requirement.  
  
 The following examples show further how to include type information in your schema.  
  
```  
<!-- 1. String with no max length -->  
<s:AttributeType name="title_id"/>  
<!-or -->  
<s:AttributeType name="title_id" dt:type="string"/>  
  
<!-- 2. Fixed length string with max length of 6 -->  
<s:AttributeType name="title_id">  
    <s:datatype dt:type="string" dt:maxLength="6" rs:fixedlength="true" />  
</s:AttributeType>  
  
<!-- 3. Variable length string with max length of 6 -->  
<s:AttributeType name="title_id">  
    <s:datatype dt:type="string" dt:maxLength="6" />  
</s:AttributeType>  
  
<!-- 4. Integer -->  
<s:AttributeType name="title_id" dt:type="int"/>  
```  
  
 There is a subtle use of the rs:fixedlength attribute in the second example. A column with the rs:fixedlength attribute set to true means that the data must have the length defined in the schema. In this case, a valid value for title_id is "123456," as is "123   ." However, "123" would not be valid because its length is 3, not 6. See the OLE DB Programmer's Guide for a more complete description of the fixedlength property.  
  
## Handling Nulls  
 Null values are handled by the rs:maybenull attribute. If this attribute is set to true, the contents of the column can contain a null value. Furthermore, if the column is not found in a row of data, the user reading the data back from the rowset will get a null status from IRowset::GetData(). Consider the following column definitions from the Shippers table.  
  
```  
<s:AttributeType name="ShipperID">  
  <s:datatype dt:type="int" dt:maxLength="4"/>  
</s:AttributeType>  
<s:AttributeType name="CompanyName">  
  <s:datatype dt:type="string" dt:maxLength="40" rs:maybenull="true"/>  
</s:AttributeType>  
```  
  
 The definition allows `CompanyName` to be null, but `ShipperID` cannot contain a null value. If the data section contained the following row, the Persistence Provider would set the status of the data for the `CompanyName` column to the OLE DB status constant DBSTATUS_S_ISNULL:  
  
```  
<z:row ShipperID="1"/>  
```  
  
 If the row was entirely empty, as follows, the Persistence Provider would return an OLE DB status of DBSTATUS_E_UNAVAILABLE for `ShipperID` and DBSTATUS_S_ISNULL for CompanyName.  
  
```  
<z:row/>   
```  
  
 Note that a zero-length string is not the same as null.  
  
```  
<z:row ShipperID="1" CompanyName=""/>  
```  
  
 For the preceding row, the Persistence Provider will return an OLE DB status of DBSTATUS_S_OK for both columns. The `CompanyName` in this case is simply "" (a zero-length string).  
  
 For further information about the OLE DB constructs available for use within the schema of an XML document for OLE DB, see the definition of "urn:schemas-microsoft-com:rowset" and the OLE DB Programmer's Guide.  
  
## See Also  
 [Persisting Records in XML Format](../../../ado/guide/data/persisting-records-in-xml-format.md)
