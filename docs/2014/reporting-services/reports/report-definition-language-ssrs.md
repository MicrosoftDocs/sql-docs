---
title: "Report Definition Language (SSRS) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "SQL Server Reporting Services, RDL"
  - "Reporting Services, RDL"
  - "RDL [Reporting Services], about Report Definition Language"
  - "SSRS, RDL"
  - "Report Definition Language, about Report Definition Language"
  - "Report Definition Language"
  - "RDL [Reporting Services]"
  - "reports [Reporting Services], definitions"
ms.assetid: b18b025e-f4bd-4744-8f86-0ac9fb967548
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Report Definition Language (SSRS)
  Report Definition Language (RDL) is an XML representation of a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] report definition. A report definition contains data retrieval and layout information for a report. RDL is composed of XML elements that match an XML grammar created for [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]. You can add your own custom functions for controlling report item values, styles, and formatting by accessing code assemblies within report definition files.  
  
 RDL promotes the interoperability of commercial reporting products by defining a common schema that enables the interchange of report definitions. Any protocol or programmatic interface that works with XML can be used with RDL. RDL is:  
  
-   An XML schema for report definitions.  
  
-   An interchange format for businesses and third parties.  
  
-   An extensible and open schema that supports additional namespaces and custom elements.  
  
##  <a name="bkmk_RDL_Specifications"></a> RDL Specifications  
 To download specifications for specific schema versions, see [Report Definition Language Specification](https://go.microsoft.com/fwlink/?linkid=116865).  
  
##  <a name="bkmk_RDL_XML_Schema_Definition"></a> RDL XML Schema Definition  
 A [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Report Definition Language (RDL) file is validated by using an XML Schema Definition (XSD) file. The schema defines the rules for where RDL elements can occur in an .rdl file. An element includes its data type and cardinality, that is, the number of occurrences that are allowed. An element can be simple or complex. A simple element does not have child elements or attributes. A complex element does have children and optionally, attributes.  
  
 For example, the schema includes the RDL element `ReportParameters`, which is the complex type `ReportParametersType`. By convention, a complex type for an element is the name of the element follow by the word `Type`. A `ReportParameters` element can be contained by the `Report` element (a complex type), and can contain `ReportParameter` elements. A `ReportParameterType` is a simple type that can only be one of the following values: `Boolean`, `DateTime`, `Integer`, `Float`, or `String`. For more information about XML Schema datatypes, see [XML Schema Part 2: Datatypes Second Edition](https://go.microsoft.com/fwlink/?linkid=4871).  
  
 The RDL XSD is available in the ReportDefinition.xsd file, located in the Extras folder on the product CD-ROM. It is also available on the report server through the following URL: http://servername/reportserver/reportdefinition.xsd.  
  
##  <a name="bkmk_Creating_RDL"></a> Creating RDL  
 Because of the open and extensible nature of RDL, a variety of tools and applications can be built that generate RDL based on its XML schema.  
  
 [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] provides multiple tools to build RDL files. For more information, see [Reporting Services Tools](../tools/reporting-services-tools.md).  
  
 One of the easiest ways to generate RDL from an application is to use the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] classes of the <xref:System.Xml> namespace and <xref:System.Linq> namespace. One class in particular, the **XmlTextWriter** class, can be used to write RDL. With **XmlTextWriter**, you can generate a complete report definition from start to finish in any [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] application. Developers can also extend RDL by adding custom report items with custom properties. For more on the **XmlTextWriter** class and the <xref:System.Xml> namespace, see the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] Developer's Guide. For more information about Language-Integrated Query (LINQ), search for "LINQ to XML" on MSDN.  
  
 The standard file extension for report definition files is .rdl. You can also develop client report definition files, which have the extensions .rdlc. The MIME type for both extensions is text/xml. For more information about reports, see [Reporting Services Reports &#40;SSRS&#41;](reporting-services-reports-ssrs.md).  
  
##  <a name="bkmk_RDL_Types"></a> RDL Types  
 The following table lists types used in RDL elements and attributes.  
  
|Type|Description|  
|----------|-----------------|  
|`Binary`|A property with a base-64 encoded binary value.|  
|`Boolean`|A property with `true` or `false` as the value of the object. Unless specified otherwise, the value of an omitted optional Boolean object is `False`.|  
|`Date`|A property with a fully specified date or datetime value specified in ISO8601 date format: YYYY-MM-DD[THH:MM[:SS[.S]]].|  
|`Enum`|A property with a string text value that must be one of a list of designated values.|  
|`Float`|A property with a float value. A period (.) is used as the optional decimal separator.|  
|`Integer`|A property with an integer (int32) value.|  
|`Language`|A property with a text value that contains a language and culture code, such as "en-us" for US English. The value must either be a specific language or a neutral language for which a default language is defined in the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)].|  
|`Name`|A property with a string text value. Names must be unique within the namespace of the item. If not specified, the namespace for an item is the innermost containing object that has a name.|  
|`NormalizedString`|A property with a string text value that has been normalized.|  
|`Size`|A size element must contain a number (with a period character used as an optional decimal separator). The number must be followed by a designator for a CSS length unit such as cm, mm, in, pt, or pc. A space between the number and the designator is optional. For more information about size designators, see [CSS Length Units Reference](https://www.w3schools.com/CSSref/css_units.asp).<br /><br /> In RDL, the maximum value for `Size` is 160 in. The minimum size is 0 in.|  
|`String`|A property with a string text value.|  
|`UnsignedInt`|A property with an unsigned integer (uint32) value.|  
|`Variant`|A property with any simple XML type.|  
  
##  <a name="bkmk_RDL_Data_Types"></a> RDL Data Types  
 The DataType Enumeration defines the data type of an attribute, expression, or parameter in RDL. The following table shows how common language runtime (CLR) data types correspond to RDL data types.  
  
|**CLR Type(s)**|**Corresponding Data Type**|  
|-----------------------|---------------------------------|  
|Boolean|Boolean|  
|DateTime, DateTimeOffset|DateTime|  
|Int16, Int32, UInt16, Byte, SByte|Integer|  
|Single, Double|Float|  
|String, Char, GUID, Timespan|String|  
  
## See Also  
 [Find the Report Definition Schema Version &#40;SSRS&#41;](find-the-report-definition-schema-version-ssrs.md)   
 [Using Custom Assemblies with Reports](../custom-assemblies/using-custom-assemblies-with-reports.md)   
 [Custom Report Items](../custom-report-items/custom-report-items.md)  
  
  
