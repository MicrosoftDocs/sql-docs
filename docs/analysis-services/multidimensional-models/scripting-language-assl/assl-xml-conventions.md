---
title: "ASSL XML Conventions | Microsoft Docs"
ms.date: 05/02/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: xmla
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# ASSL XML Conventions
[!INCLUDE[ssas-appliesto-sqlas](../../../includes/ssas-appliesto-sqlas.md)]
  Analysis Services Scripting Language (ASSL) represents the hierarchy of objects as a set of element types, each of which defines the child elements they can contain.  
  
 To represent the object hierarchy, ASSL uses the following XML conventions:  
  
-   All objects and properties are represented as elements, except for standard XML attributes such as 'xml:lang'.  
  
-   Both element names and enumeration values follow the Microsoft .NET Framework naming convention of Pascal casing with no underscores.  
  
-   The case of all values is preserved. Values for enumerations are also case-sensitive.  
  
 In addition to this list of conventions, Analysis Services also follows certain conventions regarding cardinality, inheritance, whitespace, data types, and default values.  
  
## Cardinality  
 When an element has a cardinality that is greater than 1, there is an XML element collection that encapsulates this element. The name of collection uses the plural form of the elements contained in the collection. For example, the following XML fragment represents the **Dimensions** collection within a **Database** element:  
  
 `<Database>`  
  
 `...`  
  
 `<Dimensions>`  
  
 `<Dimension>`  
  
 `...`  
  
 `</Dimension>`  
  
 `<Dimension>`  
  
 `...`  
  
 `</Dimension>`  
  
 `</Dimensions>`  
  
 `</Database>`  
  
 ``  
  
 The order in which elements appear is unimportant.  
  
## Inheritance  
 Inheritance is used when there are distinct objects that have overlapping but significantly different sets of properties. Examples of such overlapping but distinct objects are virtual cubes, linked cubes, and regular cubes. For overlapping but distinct object, Analysis Services uses the standard **type** attribute from the XML Instance Namespace to indicate the inheritance. For example, the following XML fragment shows how the **type** attribute identifies whether a **Cube** element inherits from a regular cube or from a virtual cube:  
  
 `<Cubes>`  
  
 `<Cube xsi:type="RegularCube">`  
  
 `<Name>Sales</Name>`  
  
 `...`  
  
 `</Cube>`  
  
 `<Cube xsi:type="VirtualCube">`  
  
 `<Name>SalesAndInventory</Name>`  
  
 `...`  
  
 `</Cube>`  
  
 `</Cubes>`  
  
 ``  
  
 Inheritance is generally not used when multiple types have a property of the same name. For example, the **Name** and **ID** properties appear on many elements, but these properties have not been promoted to an abstract type.  
  
## Whitespace  
 Whitespace within an element value is preserved. However, leading and trailing whitespace is always trimmed. For example, the following elements have the same text but differing amounts of whitespace within that text, and are therefore treated as if they have different values:  
  
 `<Description>My text<Description>`  
  
 `<Description>My  text<Description>`  
  
 ``  
  
 However, the following elements vary only in leading and trailing whitespace, and are therefore treated as if they have equivalent values:  
  
 `<Description>My text<Description>`  
  
 `<Description>  My text  <Description>`  
  
 ``  
  
## Data Types  
 Analysis Services uses the following standard XML Schema definition language (XSD) data types:  
  
 **Int**  
 An integer value in the range of -231 to 231 - 1.  
  
 **Long**  
 An integer value in range of -263 to 263 - 1.  
  
 **String**  
 A string value that conforms to the following global rules:  
  
-   Control characters are stripped out.  
  
-   Leading and trailing white space is trimmed.  
  
-   Internal white space is preserved.  
  
 **Name** and **ID** properties have special limitations on valid characters in string elements. For additional information about **Name** and **ID** conventions, see [ASSL Objects and Object Characteristics](../../../analysis-services/multidimensional-models/scripting-language-assl/assl-objects-and-object-characteristics.md).  
  
 **DateTime**  
 A **DateTime** structure from the .NET Framework. A **DateTime** value cannot be NULL. The lowest date supported by the **DataTime** data type is January 1, 1601, which is available to programmers as **DateTime.MinValue**. The lowest supported date indicates that a **DateTime** value is missing.  
  
 **Boolean**  
 An enumeration with only two values, such as {true, false} or {0, 1}.  
  
## Default Values  
 Analysis Services uses the defaults listed in the following table.  
  
|XML data type|Default value|  
|-------------------|-------------------|  
|**Boolean**|False|  
|**String**|"" (empty string)|  
|**Integer** or **Long**|0 (zero)|  
|**Timestamp**|12:00:00 AM, 1/1/0001 (corresponding to a the .NET Frameworks **System.DateTime** with 0 ticks)|  
  
 An element that is present but empty is interpreted as having a value of a null string, not the default value.  
  
### Inherited Defaults  
 Some properties that are specified on an object provide default values for the same property on child or descendant objects. For example, **Cube.StorageMode** provides the default value for **Partition.StorageMode**. The rules that Analysis Services applies for inherited default values are as follows:  
  
-   When the property for the child object is null in the XML, its value defaults to the inherited value. However, if you query the value from the server, the server returns the null value of the XML element.  
  
-   It is not possible to determine programmatically whether the property of a child object has been set directly on the child object or inherited.  
  
 Some elements have defined defaults that apply when the element is missing. For example, the **Dimension** elements in the following XML fragment are equivalent even though one **Dimension** element contains a **Visible** element, but the other **Dimension** element does not.  
  
 `<Dimension>`  
  
 `<Name>Product</Name>`  
  
 `</Dimension>`  
  
 `<Dimension>`  
  
 `<Name>Product</ Name>`  
  
 `<Visible>true</Visible>`  
  
 `</Dimension>`  
  
 For more information on inherited defaults, see [ASSL Objects and Object Characteristics](../../../analysis-services/multidimensional-models/scripting-language-assl/assl-objects-and-object-characteristics.md).  
  
  
