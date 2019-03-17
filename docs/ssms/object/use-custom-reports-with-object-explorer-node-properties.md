---
title: "Use Custom Reports with Object Explorer Node Properties | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "SQL Server Management Studio [SQL Server], custom reports"
ms.assetid: c7b84355-71ba-402d-85af-23826f18b7da
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# Use Custom Reports with Object Explorer Node Properties
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
Custom reports can execute in the context of a selected Object Explorer node if the custom reports reference the report parameters of that node. This enables a custom report to use the current context, for example the current database, or a database or server object.  
  
## Object Explorer Node Report Parameters  
  
|Parameter name|Data type|  
|------------------|-------------|  
|**ObjectName**|**String**|  
|**ObjectTypeName**|**String**|  
|**Filtered**|**Boolean**|  
|**ServerName**|**String**|  
|**FontName**|**String**|  
|**DatabaseName**|**String**|  
  
## Object Explorer Node Report Parameters Example  
To run the example, use the following procedure.  
  
**To view the report parameter values for a node in Object Explorer**  
  
1.  Copy the following sample code into a new text file and name the file by using an .rdl extension.  
  
2.  Copy the report file to a folder that you have created on the database server for custom reports.  
  
3.  In [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)], right-click a node in Object Explorer, point to **Reports**, click Custom Reports. In the **Open File** dialog box, locate the custom reports folder and select the report file, and then click **Open**.  
  
    When a new custom report is first opened from an Object Explorer node, the custom report is added to the most recently used list under **Custom Reports** on the shortcut menu of that node. When a standard report is opened for the first time, it will also appear on the most recently used list under **Custom Reports**. If a custom report file is deleted, the next time that the item is selected, a prompt will appear to delete the item from the most recently used list.  
  
    1.  To change the number of files displayed on the recently used list, on the **Tools** menu, click **Options**, expand the **Environment** folder, and then click **General**.  
  
    2.  Adjust the number for **Display files in recently used list**.  
  
## Custom Report Code Sample  
The report that is created by using the following code will use the parameters that are associated with an Object Explorer node.  
  
```  
<pre><?xml version="1.0" encoding="utf-8"?>  
<Report xmlns="https://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition" xmlns:rd="https://schemas.microsoft.com/SQLServer/reporting/reportdesigner">  
<ReportParameters>  
<ReportParameter Name="ObjectName">  
<DataType>String</DataType>  
<Nullable>true</Nullable>  
<AllowBlank>true</AllowBlank>  
<Prompt>ObjectName</Prompt>  
</ReportParameter>  
<ReportParameter Name="ObjectTypeName">  
<DataType>String</DataType>  
<Nullable>true</Nullable>  
<AllowBlank>true</AllowBlank>  
<Prompt>ObjectTypeName</Prompt>  
</ReportParameter>  
<ReportParameter Name="Filtered">  
<DataType>Boolean</DataType>  
<Nullable>true</Nullable>  
<AllowBlank>true</AllowBlank>  
<Prompt>Filtered</Prompt>  
</ReportParameter>  
<ReportParameter Name="ServerName">  
<DataType>String</DataType>  
<Nullable>true</Nullable>  
<AllowBlank>true</AllowBlank>  
<Prompt>ServerName</Prompt>  
</ReportParameter>  
<ReportParameter Name="FontName">  
<DataType>String</DataType>  
<DefaultValue>  
<Values>  
<Value>Tahoma</Value>  
</Values>  
</DefaultValue>  
<AllowBlank>true</AllowBlank>  
<Prompt>FontName</Prompt>  
</ReportParameter>  
<ReportParameter Name="DatabaseName">  
<DataType>String</DataType>  
<Nullable>true</Nullable>  
<DefaultValue>  
<Values>  
<Value>master</Value>  
</Values>  
</DefaultValue>  
<AllowBlank>true</AllowBlank>  
<Prompt>DatabaseName</Prompt>  
</ReportParameter>  
</ReportParameters>  
<DataSources>  
<DataSource Name="AllReportParameters">  
<ConnectionProperties>  
<IntegratedSecurity>true</IntegratedSecurity>  
<ConnectString>Data Source=.</ConnectString>  
<DataProvider>SQL</DataProvider>  
</ConnectionProperties>  
<rd:DataSourceID>f1feee4c-0fdc-4301-9efa-3cd89eed2d9f</rd:DataSourceID>  
</DataSource>  
</DataSources>  
<BottomMargin>1in</BottomMargin>  
<RightMargin>1in</RightMargin>  
<rd:DrawGrid>true</rd:DrawGrid>  
<InteractiveWidth>8.5in</InteractiveWidth>  
<rd:SnapToGrid>true</rd:SnapToGrid>  
<Body>  
<ReportItems>  
<Textbox Name="textbox1">  
<rd:DefaultName>textbox1</rd:DefaultName>  
<ZIndex>1</ZIndex>  
<Width>6in</Width>  
<Style>  
<PaddingLeft>2pt</PaddingLeft>  
<PaddingBottom>2pt</PaddingBottom>  
<FontFamily>Tahoma</FontFamily>  
<FontWeight>700</FontWeight>  
<FontSize>20pt</FontSize>  
<Color>SteelBlue</Color>  
<PaddingRight>2pt</PaddingRight>  
<PaddingTop>2pt</PaddingTop>  
</Style>  
<CanGrow>true</CanGrow>  
<Height>0.36in</Height>  
<Value>AllReportParameters</Value>  
</Textbox>  
<Table Name="table1">  
<DataSetName>AllReportParameters</DataSetName>  
<Top>0.36in</Top>  
<Details>  
<TableRows>  
<TableRow>  
<TableCells>  
<TableCell>  
<ReportItems>  
<Textbox Name="ObjectName">  
<rd:DefaultName>ObjectName</rd:DefaultName>  
<ZIndex>5</ZIndex>  
<Style>  
<BorderStyle>  
<Default>Solid</Default>  
</BorderStyle>  
<PaddingLeft>2pt</PaddingLeft>  
<PaddingBottom>2pt</PaddingBottom>  
<FontFamily>Tahoma</FontFamily>  
<BorderColor>  
<Default>LightGrey</Default>  
</BorderColor>  
<PaddingRight>2pt</PaddingRight>  
<PaddingTop>2pt</PaddingTop>  
</Style>  
<CanGrow>true</CanGrow>  
<Value>=Parameters!ObjectName.Value</Value>  
</Textbox>  
</ReportItems>  
</TableCell>  
<TableCell>  
<ReportItems>  
<Textbox Name="ObjectTypeName">  
<rd:DefaultName>ObjectTypeName</rd:DefaultName>  
<ZIndex>4</ZIndex>  
<Style>  
<BorderStyle>  
<Default>Solid</Default>  
</BorderStyle>  
<PaddingLeft>2pt</PaddingLeft>  
<PaddingBottom>2pt</PaddingBottom>  
<FontFamily>Tahoma</FontFamily>  
<BorderColor>  
<Default>LightGrey</Default>  
</BorderColor>  
<PaddingRight>2pt</PaddingRight>  
<PaddingTop>2pt</PaddingTop>  
</Style>  
<CanGrow>true</CanGrow>  
<Value>=Parameters!ObjectTypeName.Value</Value>  
</Textbox>  
</ReportItems>  
</TableCell>  
<TableCell>  
<ReportItems>  
<Textbox Name="Filtered">  
<rd:DefaultName>Filtered</rd:DefaultName>  
<ZIndex>3</ZIndex>  
<Style>  
<BorderStyle>  
<Default>Solid</Default>  
</BorderStyle>  
<PaddingLeft>2pt</PaddingLeft>  
<PaddingBottom>2pt</PaddingBottom>  
<FontFamily>Tahoma</FontFamily>  
<BorderColor>  
<Default>LightGrey</Default>  
</BorderColor>  
<PaddingRight>2pt</PaddingRight>  
<PaddingTop>2pt</PaddingTop>  
</Style>  
<CanGrow>true</CanGrow>  
<Value>=Parameters!Filtered.Value</Value>  
</Textbox>  
</ReportItems>  
</TableCell>  
<TableCell>  
<ReportItems>  
<Textbox Name="ServerName">  
<rd:DefaultName>ServerName</rd:DefaultName>  
<ZIndex>2</ZIndex>  
<Style>  
<BorderStyle>  
<Default>Solid</Default>  
</BorderStyle>  
<PaddingLeft>2pt</PaddingLeft>  
<PaddingBottom>2pt</PaddingBottom>  
<FontFamily>Tahoma</FontFamily>  
<BorderColor>  
<Default>LightGrey</Default>  
</BorderColor>  
<PaddingRight>2pt</PaddingRight>  
<PaddingTop>2pt</PaddingTop>  
</Style>  
<CanGrow>true</CanGrow>  
<Value>=Parameters!ServerName.Value</Value>  
</Textbox>  
</ReportItems>  
</TableCell>  
<TableCell>  
<ReportItems>  
<Textbox Name="FontName">  
<rd:DefaultName>FontName</rd:DefaultName>  
<ZIndex>1</ZIndex>  
<Style>  
<BorderStyle>  
<Default>Solid</Default>  
</BorderStyle>  
<PaddingLeft>2pt</PaddingLeft>  
<PaddingBottom>2pt</PaddingBottom>  
<FontFamily>Tahoma</FontFamily>  
<BorderColor>  
<Default>LightGrey</Default>  
</BorderColor>  
<PaddingRight>2pt</PaddingRight>  
<PaddingTop>2pt</PaddingTop>  
</Style>  
<CanGrow>true</CanGrow>  
<Value>=Parameters!FontName.Value</Value>  
</Textbox>  
</ReportItems>  
</TableCell>  
<TableCell>  
<ReportItems>  
<Textbox Name="DatabaseName">  
<rd:DefaultName>DatabaseName</rd:DefaultName>  
<Style>  
<BorderStyle>  
<Default>Solid</Default>  
</BorderStyle>  
<PaddingLeft>2pt</PaddingLeft>  
<PaddingBottom>2pt</PaddingBottom>  
<FontFamily>Tahoma</FontFamily>  
<BorderColor>  
<Default>LightGrey</Default>  
</BorderColor>  
<PaddingRight>2pt</PaddingRight>  
<PaddingTop>2pt</PaddingTop>  
</Style>  
<CanGrow>true</CanGrow>  
<Value>=Parameters!DatabaseName.Value</Value>  
</Textbox>  
</ReportItems>  
</TableCell>  
</TableCells>  
<Height>0.21in</Height>  
</TableRow>  
</TableRows>  
</Details>  
<Header>  
<TableRows>  
<TableRow>  
<TableCells>  
<TableCell>  
<ReportItems>  
<Textbox Name="textbox2">  
<rd:DefaultName>textbox2</rd:DefaultName>  
<ZIndex>11</ZIndex>  
<Style>  
<BorderStyle>  
<Default>Solid</Default>  
</BorderStyle>  
<PaddingLeft>2pt</PaddingLeft>  
<PaddingBottom>2pt</PaddingBottom>  
<FontFamily>Tahoma</FontFamily>  
<FontWeight>700</FontWeight>  
<FontSize>11pt</FontSize>  
<BorderColor>  
<Default>LightGrey</Default>  
</BorderColor>  
<BackgroundColor>SteelBlue</BackgroundColor>  
<Color>White</Color>  
<PaddingRight>2pt</PaddingRight>  
<PaddingTop>2pt</PaddingTop>  
</Style>  
<CanGrow>true</CanGrow>  
<Value>ObjectName</Value>  
</Textbox>  
</ReportItems>  
</TableCell>  
<TableCell>  
<ReportItems>  
<Textbox Name="textbox3">  
<rd:DefaultName>textbox3</rd:DefaultName>  
<ZIndex>10</ZIndex>  
<Style>  
<BorderStyle>  
<Default>Solid</Default>  
</BorderStyle>  
<PaddingLeft>2pt</PaddingLeft>  
<PaddingBottom>2pt</PaddingBottom>  
<FontFamily>Tahoma</FontFamily>  
<FontWeight>700</FontWeight>  
<FontSize>11pt</FontSize>  
<BorderColor>  
<Default>LightGrey</Default>  
</BorderColor>  
<BackgroundColor>SteelBlue</BackgroundColor>  
<Color>White</Color>  
<PaddingRight>2pt</PaddingRight>  
<PaddingTop>2pt</PaddingTop>  
</Style>  
<CanGrow>true</CanGrow>  
<Value>ObjectTypeName</Value>  
</Textbox>  
</ReportItems>  
</TableCell>  
<TableCell>  
<ReportItems>  
<Textbox Name="textbox4">  
<rd:DefaultName>textbox4</rd:DefaultName>  
<ZIndex>9</ZIndex>  
<Style>  
<BorderStyle>  
<Default>Solid</Default>  
</BorderStyle>  
<PaddingLeft>2pt</PaddingLeft>  
<PaddingBottom>2pt</PaddingBottom>  
<FontFamily>Tahoma</FontFamily>  
<FontWeight>700</FontWeight>  
<FontSize>11pt</FontSize>  
<BorderColor>  
<Default>LightGrey</Default>  
</BorderColor>  
<BackgroundColor>SteelBlue</BackgroundColor>  
<Color>White</Color>  
<PaddingRight>2pt</PaddingRight>  
<PaddingTop>2pt</PaddingTop>  
</Style>  
<CanGrow>true</CanGrow>  
<Value>Filtered</Value>  
</Textbox>  
</ReportItems>  
</TableCell>  
<TableCell>  
<ReportItems>  
<Textbox Name="textbox5">  
<rd:DefaultName>textbox5</rd:DefaultName>  
<ZIndex>8</ZIndex>  
<Style>  
<BorderStyle>  
<Default>Solid</Default>  
</BorderStyle>  
<PaddingLeft>2pt</PaddingLeft>  
<PaddingBottom>2pt</PaddingBottom>  
<FontFamily>Tahoma</FontFamily>  
<FontWeight>700</FontWeight>  
<FontSize>11pt</FontSize>  
<BorderColor>  
<Default>LightGrey</Default>  
</BorderColor>  
<BackgroundColor>SteelBlue</BackgroundColor>  
<Color>White</Color>  
<PaddingRight>2pt</PaddingRight>  
<PaddingTop>2pt</PaddingTop>  
</Style>  
<CanGrow>true</CanGrow>  
<Value>ServerName</Value>  
</Textbox>  
</ReportItems>  
</TableCell>  
<TableCell>  
<ReportItems>  
<Textbox Name="textbox6">  
<rd:DefaultName>textbox6</rd:DefaultName>  
<ZIndex>7</ZIndex>  
<Style>  
<BorderStyle>  
<Default>Solid</Default>  
</BorderStyle>  
<PaddingLeft>2pt</PaddingLeft>  
<PaddingBottom>2pt</PaddingBottom>  
<FontFamily>Tahoma</FontFamily>  
<FontWeight>700</FontWeight>  
<FontSize>11pt</FontSize>  
<BorderColor>  
<Default>LightGrey</Default>  
</BorderColor>  
<BackgroundColor>SteelBlue</BackgroundColor>  
<Color>White</Color>  
<PaddingRight>2pt</PaddingRight>  
<PaddingTop>2pt</PaddingTop>  
</Style>  
<CanGrow>true</CanGrow>  
<Value>FontName</Value>  
</Textbox>  
</ReportItems>  
</TableCell>  
<TableCell>  
<ReportItems>  
<Textbox Name="textbox7">  
<rd:DefaultName>textbox7</rd:DefaultName>  
<ZIndex>6</ZIndex>  
<Style>  
<BorderStyle>  
<Default>Solid</Default>  
</BorderStyle>  
<PaddingLeft>2pt</PaddingLeft>  
<PaddingBottom>2pt</PaddingBottom>  
<FontFamily>Tahoma</FontFamily>  
<FontWeight>700</FontWeight>  
<FontSize>11pt</FontSize>  
<BorderColor>  
<Default>LightGrey</Default>  
</BorderColor>  
<BackgroundColor>SteelBlue</BackgroundColor>  
<Color>White</Color>  
<PaddingRight>2pt</PaddingRight>  
<PaddingTop>2pt</PaddingTop>  
</Style>  
<CanGrow>true</CanGrow>  
<Value>DatabaseName</Value>  
</Textbox>  
</ReportItems>  
</TableCell>  
</TableCells>  
<Height>0.22in</Height>  
</TableRow>  
</TableRows>  
<RepeatOnNewPage>true</RepeatOnNewPage>  
</Header>  
<TableColumns>  
<TableColumn>  
<Width>3in</Width>  
</TableColumn>  
<TableColumn>  
<Width>1.5in</Width>  
</TableColumn>  
<TableColumn>  
<Width>0.75in</Width>  
</TableColumn>  
<TableColumn>  
<Width>1.125in</Width>  
</TableColumn>  
<TableColumn>  
<Width>2in</Width>  
</TableColumn>  
<TableColumn>  
<Width>1.375in</Width>  
</TableColumn>  
</TableColumns>  
</Table>  
</ReportItems>  
<Height>0.79in</Height>  
</Body>  
<rd:ReportID>abb14e58-fd36-495a-89ff-b66f29ef287b</rd:ReportID>  
<LeftMargin>1in</LeftMargin>  
<DataSets>  
<DataSet Name="AllReportParameters">  
<Query>  
<rd:UseGenericDesigner>true</rd:UseGenericDesigner>  
<CommandText>SELECT 1  
</CommandText>  
<DataSourceName>AllReportParameters</DataSourceName>  
</Query>  
<Fields>  
<Field Name="ObjectName">  
<rd:TypeName>System.String</rd:TypeName>  
<DataField>ObjectName</DataField>  
</Field>  
<Field Name="ObjectTypeName">  
<rd:TypeName>System.String</rd:TypeName>  
<DataField>ObjectTypeName</DataField>  
</Field>  
<Field Name="Filtered">  
<rd:TypeName>System.Boolean</rd:TypeName>  
<DataField>Filtered</DataField>  
</Field>  
<Field Name="ServerName">  
<rd:TypeName>System.String</rd:TypeName>  
<DataField>ServerName</DataField>  
</Field>  
<Field Name="FontName">  
<rd:TypeName>System.String</rd:TypeName>  
<DataField>FontName</DataField>  
</Field>  
<Field Name="DatabaseName">  
<rd:TypeName>System.String</rd:TypeName>  
<DataField>DatabaseName</DataField>  
</Field>  
</Fields>  
</DataSet>  
</DataSets>  
<Width>6.875in</Width>  
<InteractiveHeight>11in</InteractiveHeight>  
<Language>en-US</Language>  
<TopMargin>1in</TopMargin>  
</Report></pre>  
```

## See Also  
[Custom Reports in Management Studio](../../ssms/object/custom-reports-in-management-studio.md)  
[Add a Custom Report to Management Studio](../../ssms/object/add-a-custom-report-to-management-studio.md)  
[Unsuppress Run Custom Report Warnings](../../ssms/object/unsuppress-run-custom-report-warnings.md)  
  
