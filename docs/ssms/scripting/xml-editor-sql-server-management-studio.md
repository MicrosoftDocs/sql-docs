---
title: "XML Editor (SQL Server Management Studio) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.technology: scripting
ms.reviewer: ""
ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.editor.xml.f1"
  - "sql13.swb.editorxml.f1"
  - "vs.xmleditor"
  - "sql13.swb.xmleditor.f1"
helpviewer_keywords: 
  - "XML Designer [SQL Server Management Studio]"
ms.assetid: 0824a5ce-e67b-4b53-98d9-d371faf2d23c
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# XML Editor (SQL Server Management Studio)
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
  Provides a set of visual tools for working with XML Schemas, ADO.NET datasets, and XML documents. The XML Designer supports the XML Schema Definition (XSD) language defined by the World Wide Web Consortium (WC3). The designer does not support DTDs (document type definitions) or other XML schema languages, such as XDR (XML-Data Reduced).  
  
 To display the designer, add a dataset, XML Schema, or XML file to your project or open any of the file types listed in the table below.  
  
> [!CAUTION]  
>  There is no **Undo** command when working in Schema view. Plan your work carefully and save your files often.  
  
 The designer provides the following three views (or modes) to work on XML files, XML Schemas, and datasets:  
  
|View|Description|File types supported|  
|----------|-----------------|--------------------------|  
|**Schema**|For visually creating and modifying XML Schemas and ADO.NET datasets.|.xsd|  
|**Data**|For visually modifying XML data files in a structured data grid.|.xml|  
|**XML**|For editing XML; the source editor provides color-coding and IntelliSense, including Complete Word and List Members.|.xml .xsd .xslt .wsdl.web.resx.tdl.wsf.hta.disco.vsdisco.config|  
|**ShowPlan**|Displays xml query plans created using the SET SHOWPLAN_XML ON option.|.showplan|  
  
## Schema View  
 Schema view provides a visual representation of the elements, attributes, types, and so on, that make up XML Schemas and ADO.NET datasets.  
  
 In Schema view you can construct schemas and datasets by dropping elements on the design surface from either the XML Schema tab of the Toolbox or from Server Explorer. Additionally, you can add elements to the designer by right-clicking the design surface and selecting Add from the shortcut menu.  
  
 In Schema view you can:  
  
-   Construct and modify existing XML Schemas and ADO.NET datasets  
  
-   Create and edit relationships between tables  
  
-   Create and edit keys  
  
-   Generate ADO.NET datasets from XML Schemas  
  
> [!NOTE]  
>  The layout of elements in Schema view is stored in the .xsx file, which can be seen by clicking **Show All Files** in the Solution Explorer toolbar, and then expanding the .xsd file. If there is no .xsx file present, it means the .xsd file has never been opened in the XML Designer.  
  
### Customizing Schema View  
 The following features modify the visual layout of elements in Schema view:  
  
-   Zooming  
  
-   Expanding or collapsing of nested elements  
  
-   Automatically arranging layout of elements  
  
-   Resetting default state of collapsed elements  
  
##### To expand hidden nested elements  
  
-   Click the plus icon on the bottom of the element.  
  
##### To collapse nested elements  
  
-   Click the minus icon on the bottom-most element that you want to appear on the designer.  
  
## Data View  
 Data view provides a data grid that can be used to modify .xml files. Only the content (but not the tags and structure) in an XML file can be edited in Data view.  
  
 There are two separate areas in Data view: **Data Tables** and **Data**. The **Data Tables** area is a list of relations defined in the XML file, in the order of its nesting (from the outermost to the innermost). The **Data** area is a data grid that displays data based on the selection in the Data Tables area.  
  
> [!NOTE]  
>  Newly created XML files contain no data and therefore cannot be displayed in Data view. There are also some instances of XML documents where data view cannot be invoked at all. Although the XML would be considered well formed, if it is not structured data trying to switch to Data view will generate the following message: "Although this document is well formed, it contains structure that Data View cannot display."  
  
 In Data view you can:  
  
-   Manually populate data tables  
  
-   Edit existing data tables  
  
-   Generate an XML Schema from an XML document  
  
## XML View  
 XML view provides an editor for editing raw XML and provides IntelliSense and color coding. Statement completion is available when working on .xsd files and .xml files that have an associated schema. Type < to initiate a tag and you will be presented with a list of elements that are valid at that location. After you type the element name and press the SPACEBAR, you will be presented with a list of attributes that the element supports.  
  
> [!NOTE]  
>  [!INCLUDE[msCoName](../../includes/msconame-md.md)] IntelliSense options are not available on the toolbar. When in the XML Editor, to access the options, on the **Edit** menu, click **IntelliSense**.  
  
## SHOWPLAN view  
 Query plans can be saved in XML format when created using SET SHOWPLAN_XML ON option. Double-click a file with the .showplan extension to open the query plan.  
  
## See Also  
 [Save an Execution Plan in XML Format](../../relational-databases/performance/save-an-execution-plan-in-xml-format.md)  
  
  
