---
title: "Supported Access Report Features (SSRS) | Microsoft Docs"
ms.custom: ""
ms.date: "03/07/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "Report Designer [Reporting Services], Access reports"
  - "functions [Reporting Services]"
  - "controls [Reporting Services]"
  - "Access reports [Reporting Services]"
  - "properties [Reporting Services], Access reports"
  - "importing reports"
  - "modules [Reporting Services]"
ms.assetid: 7ffec331-6365-4c13-8e58-b77a48cffb44
author: maggiesmsft
ms.author: maghan
manager: kfile
---
# Supported Access Report Features (SSRS)
  When you import a report into Report Designer, the import process converts the [!INCLUDE[msCoName](../includes/msconame-md.md)] Access report into a [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] Report Definition Language (RDL) file. [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] supports several features of Access; however, because of differences between Access and [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)], some items are modified slightly or are not supported. This topic describes how Access report features are converted to RDL.  
  
## Importing Access Reports  
 Some queries contain code that is specific to Access. Access code is not imported with the report. Also, if a query contains embedded strings, the report may not import correctly. To correct this, replace the strings with a character code. For example, replace the comma (,) character with CHAR(34).  
  
 The import process does not properly pass the semicolon (;) or XML markup characters (\<, >, etc.) in connection string information. If a connection string contains a semicolon or XML markup character, you will have to manually set the password in the new report after the report is imported.  
  
 The import process does not import the connection or general timeout settings in the connection string. You may have to adjust these settings after the report is imported.  
  
 If you import a report that has a query that contains query parameters, the query will not be converted when the report is imported. To import the query with the report, temporarily replace the query parameters in the Access report with hard-coded values, and then replace them with query parameters after the report is imported.  
  
## Page Layout  
 Page layout in Access is different than in [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)]. Access arranges items on the page using "bands," that is, sections arranged vertically on the page. These sections may include the report header, report footer, page header, page footer, groups, and detail. [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] provides a more flexible layout. Data regions provide grouping and detail, and you can place multiple data regions anywhere in the body of the report, even side-by-side. [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] also includes a "banded" page header and footer, similar to the page header and footer in Access.  
  
 When a report is imported from Access into Report Designer, the page header and footer from the Access report are converted into a [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] report page header and footer. Groups and detail are converted into a list data region. The report header and footer are placed into the body of the report, rather than in a separate band. This may result in item placement that is slightly different than what is in the Access report.  
  
> [!NOTE]  
>  In some Access reports, report items that appear to be adjacent to each other may actually overlap. When the report is imported using Report Designer, this overlap is not corrected and may lead to unexpected results when the report is run.  
  
## Data Sources  
 [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] supports OLE DB data sources, such as [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. If you are importing reports from an Access project (.adp) file, the connection string for the data source is taken from the connection string in the .adp file. If you are importing reports from an Access database (.mdb or .accdb) file, the connection string may point to the Access database and you may have to correct it after the reports are imported. If the data source for the Access report is a query, the query information is stored without modification in the RDL. If the data source for the Access report is a table, the conversion process creates a query based on the table name and the fields in the table.  
  
## Reports with Custom Modules  
 If there is custom [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[vbprvb](../includes/vbprvb-md.md)] code contained within modules, it is not converted. If Report Designer encounters code during the import process, a warning is generated and displayed in the **Task List** window.  
  
## Report Controls  
 [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] supports the following Access controls and includes them in converted report definitions.  
  
|||||  
|-|-|-|-|  
|Image|Label|Line|Rectangle|  
|SubForm|SubReport<br /><br /> **Note** While a SubReport control is converted within the main report, the subreport itself is converted separately.|TextBox||  
  
 [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] does not support the following controls:  
  
|||||  
|-|-|-|-|  
|BoundObjectFrame|CheckBox|ComboBox|CommandButton|  
|CustomControl|ListBox|ObjectFrame|OptionButton|  
|TabControl|ToggleButton|||  
  
 If Report Designer encounters any of these controls during the import process, a warning is generated and displayed in the **Task List** window.  
  
 Other controls, like ActiveX and Office Web Components, are not imported. For example, if an Access report contains an OWC Chart control, it will not be converted when the report is imported.  
  
## Report Properties  
 [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] supports the following properties, which are available through the Access user interface. Properties available only in code are not supported and are not listed here.  
  
|||||  
|-|-|-|-|  
|BackColor|BackStyle|BorderColor|BorderStyle|  
|BorderWidth|BottomMargin|CanGrow (textbox)|CanShrink (textbox)|  
|Caption|FontBold|FontItalic|FontName|  
|FontSize|FontUnderline|FontWeight|ForceNewPage|  
|ForeColor|Height|HideDuplicates|Hyperlink|  
|IsHyperlink|IsVisible|KeepTogether (group)|Left|  
|LeftMargin|LineSlant|LineSpacing|LinkChildFields|  
|LinkMasterFields|NewRowOrCol|PageFooter|PageHeader|  
|Pages|Picture|PictureTiling (report)|ReadingOrder|  
|RepeatSection|RightMargin|RunningSum|SizeMode|  
|TextAlign|Top|TopMargin|Width|  
  
 [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] does not support the following properties, which are available through the Access user interface.  
  
|||||  
|-|-|-|-|  
|CanGrow (section)|CanShrink (section)|DecimalPlaces|FastLaserPrinting|  
|Filter|FilterOn|Format|FormatConditions|  
|GrpKeepTogether|KeepTogether (section)|NumeralShapes|Orientation|  
|PaintPalette|PaletteSource|PictureAlignment|PicturePages|  
|PictureSizeMode|PictureTiling (image)|ScrollBars|SpecialEffect|  
|Vertical||||  
  
## Grouping  
 Access defines a group level using a combination of three properties: the group expression, the `GroupOn` property, and the `GroupInterval` property. A group that does not have a group header or footer is merged with the group contained within it. If the group does not contain another group, sorting is applied to the detail section and the group is dropped.  
  
## Expressions  
 Access uses expressions to specify values that appear in text boxes. Access uses [!INCLUDE[vbprvb](../includes/vbprvb-md.md)] as its expression language in addition to some aggregate functions. Report Designer converts these Access expressions to report expressions.  
  
### Functions  
 A [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] report definition uses [!INCLUDE[vbprvb](../includes/vbprvb-md.md)] .NET as its native expression language, while Access 2002 uses Visual Basic. The following lists describe the functions that are supported by [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)].  
  
#### Array Functions  
 [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] supports the following array functions:  
  
-   LBound  
  
-   UBound  
  
#### Conversion Functions  
 [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] supports the following conversion functions.  
  
|||||  
|-|-|-|-|  
|Asc|CBool|CByte|CCur|  
|CDate|CDbl|CDec|Chr|  
|Chr$|CInt|CLng|CSng|  
|CStr|CVar|CVDate|Format|  
|FormatCurrency|FormatDateTime|FormatNumber|FormatPercent|  
|Hex|Hex$|Nz|Oct|  
|Oct$|Str|Str$|StrConv|  
|Val||||  
  
 [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] does not support the following conversion functions:  
  
-   GUIDFromString  
  
-   StringFromGUID  
  
#### Database Functions  
 [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] supports the following database functions.  
  
|||||  
|-|-|-|-|  
|CreateReport|GetObject|HyperlinkPart|Partition|  
  
 [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] does not support the following database functions.  
  
|||||  
|-|-|-|-|  
|CodeDb|CreateControl|CreateForm|CreateGroupLevel|  
|CreateObject|CreateReportControl|CurrentDb|CurrentUser|  
|DeleteControl|DeleteReportControl|Eval|IMEStatus|  
|SysCmd||||  
  
#### Date/Time Functions  
 [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] supports the following date/time functions.  
  
|||||  
|-|-|-|-|  
|Date|Date$|DateAdd|DateDiff|  
|DatePart|DateSerial|DateValue|Day|  
|Hour|Minute|Month|MonthName|  
|Now|Second|Time|Time$|  
|Timer|TimeSerial|TimeValue|Weekday|  
|WeekdayName|Year|||  
  
#### DDE/OLE Functions  
 [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] does not support the following DDE/OLE functions.  
  
|||||  
|-|-|-|-|  
|DDE|DDEIntitate|DDERequest|DDESend|  
|LoadPicture||||  
  
#### Domain Aggregate Functions  
 [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] does not support the following domain aggregate functions.  
  
|||||  
|-|-|-|-|  
|DAvg|DCount|DFirst|DLast|  
|DLookup|DMax|DMin|DStDev|  
|DStDevP|DSum|DVar|DVarP|  
  
#### Error Handling Functions  
 [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] supports the following error handling functions.  
  
|||||  
|-|-|-|-|  
|Err|Error|Error$|IsError|  
  
 [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] does not support the following error handling function:  
  
-   CVErr  
  
#### Financial Functions  
 [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] supports the following financial functions.  
  
|||||  
|-|-|-|-|  
|DDB|FV|IPmt|IRR|  
|MIRR|NPer|NPV|Pmt|  
|PPmt|PV|Rate|SLN|  
|SYD||||  
  
#### Interaction Functions  
 [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] supports the following interaction functions.  
  
|||||  
|-|-|-|-|  
|Command|Command$|CurDir|CurDir$|  
|DeleteSetting|Dir|Dir$|Environ|  
|Environ$|EOF|FileAttr|FileDateTime|  
|FileLen|FreeFile|GetAllSettings|GetAttr|  
|GetSetting|Loc|LOF|QBColor|  
|RGB|SaveSetting|Seek|SetAttr|  
|Shell|Spc|Tab||  
  
 [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] does not support the following interaction functions.  
  
|||||  
|-|-|-|-|  
|DoEvents|In|Input|Input$|  
  
#### Inspection Functions  
 [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] supports the following inspection functions.  
  
|||||  
|-|-|-|-|  
|IsArray|IsDate|IsEmpty|IsError|  
|IsNull|IsNumeric|IsObject|TypeName|  
|VarType||||  
  
 [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] does not support the following inspection function:  
  
-   IsMissing  
  
#### Math Functions  
 [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] supports the following math functions.  
  
|||||  
|-|-|-|-|  
|Abs|Atn|Cos|Exp|  
|Fix|Int|Log|Rnd|  
|Round|Sgn|Sin|Sqr|  
|Tan||||  
  
#### Message Functions  
 [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] does not support the following message functions.  
  
|||||  
|-|-|-|-|  
|InputBox|InputBox$|MsgBox||  
  
#### Program Flow Functions  
 [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] supports the following program flow functions.  
  
|||||  
|-|-|-|-|  
|Choose|IIf|Switch||  
  
#### SQL Aggregate Functions  
 [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] supports the following SQL aggregate functions.  
  
|||||  
|-|-|-|-|  
|Avg|Count|Max|Min|  
|StDev|StDevP|Sum|Var|  
|VarP||||  
  
#### Text Functions  
 [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] supports the following text functions.  
  
|||||  
|-|-|-|-|  
|Format|Format$|InStr|InStrRev|  
|LCase|LCase$|Left|Left$|  
|Len|LTrim|LTrim$|Mid|  
|Mid$|Replace|Right|Right$|  
|RTrim|Space|Space$|StrComp|  
|StrConv|String|String$|StrReverse|  
|Trim|Trim$|UCase|UCase$|  
  
### Constants  
 Access does not support special [!INCLUDE[vbprvb](../includes/vbprvb-md.md)] constants (for example, `vbTrue`) in expressions, so no conversion is necessary. However, there is one exception: the keyword `Null` is converted to `System.DbNull.Value`.  
  
### Parameters  
 During the import process, Report Designer scans each expression within a report for variables that do not correspond to field names or controls. These variables are added to report parameters.  
  
 The data type for stored procedure parameters is always imported as string. After the report is imported, you must manually change the parameter to use the correct data type.  
  
### Object Names  
 Access allows fields to have the same name as controls; [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] does not. [!INCLUDE[vbprvb](../includes/vbprvb-md.md)] 6.0 allows spaces in variable names; Visual Basic .NET does not. The import process replaces the names of all such objects with valid names and assigns unique names if more than one object has the same name. Each expression is scanned and the names of variables that correspond to renamed objects are replaced with the new names.  
  
## Rectangles and Containment  
 In a [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] report definition, rectangles can contain other report items. Any rectangle larger than the report item and which overlaps more than 90 percent of its area becomes a container for the report item.  
  
## Bitmaps  
 All bitmaps that are embedded within a report are converted to .bmp format when the report is imported, regardless of their initial format. For example, if your report includes .jpg and .gif files, the resulting resources imported with the report are .bmp files. The bitmaps are stored as embedded images in the report. For information about Embedded Images, see [Images &#40;Report Builder and SSRS&#41;](report-design/images-report-builder-and-ssrs.md).  
  
## Other Considerations  
 In addition to the previous items, the following information applies to reports imported from Access:  
  
-   Conditional formatting is not converted.  
  
-   The description field in report properties in Access is not converted.  
  
  
