---
title: "Type Element (DimensionAttribute) (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "Type Element (DimensionAttribute)"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "TYPE"
helpviewer_keywords: 
  - "Type element"
ms.assetid: 64fce1f5-39b7-4d0a-ae60-21203a03bd0d
caps.latest.revision: 35
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Type Element (DimensionAttribute) (ASSL)
  Contains the type of the attribute.  
  
## Syntax  
  
```xml  
  
<DimensionAttribute>  
      ...  
   <Type>...</Type>  
   ...  
</DimensionAttribute>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String (enumeration)|  
|Default value|*Regular*|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[DimensionAttribute](../../../analysis-services/scripting/data-type/dimensionattribute-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The value of this element is limited to one of the strings listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|*Account*|The attribute represents the name of an account.|  
|*AccountNumber*|The attribute represents the number of an account.|  
|*AccountType*|The attribute represents the type of an account.|  
|*Address*|The attribute represents an address.|  
|*AddressBuilding*|The attribute represents a building identifier for an address.|  
|*AddressCity*|The attribute represents a city for an address.|  
|*AddressCountry*|The attribute represents a country or region for an address.|  
|*AddressFax*|The attribute represents a fax telephone number.|  
|*AddressFloor*|The attribute represents a floor identifier for an address.|  
|*AddressHouse*|The attribute represents a house number for an address.|  
|*AddressPhone*|The attribute represents a telephone number.|  
|*AddressQuarter*|The attribute represents a quarter for an address.|  
|*AddressRoom*|The attribute represents a room identifier for an address.|  
|*AddressStateOrProvince*|The attribute represents a state or province for an address.|  
|*AddressStreet*|The attribute represents the street for an address.|  
|*AddressZip*|The attribute represents a postal code (ZIP code) for an address.|  
|*BOMResource*|The attribute represents a resource for a bill of materials (BOM).|  
|*Caption*|The attribute represents a caption.|  
|*CaptionAbbreviation*|The attribute represents an abbreviation.|  
|*CaptionDescription*|The attribute represents a description.|  
|*Channel*|The attribute represents a channel.|  
|*City*|The attribute represents a city.|  
|*Company*|The attribute represents a company.|  
|*Continent*|The attribute represents a continent.|  
|*Country*|The attribute represents a country or region.|  
|*County*|The attribute represents a county.|  
|*CurrencyDestination*|The attribute represents the destination currency of a currency exchange.|  
|*CurrencyISOcode*|The attribute represents the ISO code of a currency.|  
|*CurrencName*|The attribute represents the name of a currency.|  
|*CurrencySource*|The attribute represents the source currency of a currency exchange.|  
|*CustomerGroup*|The attribute represents a group of customers.|  
|*CustomerHousehold*|The attribute represents a household of customers.|  
|*Customers*|The attribute represents a customer.|  
|*Date*|The attribute represents a date.|  
|*DateCanceled*|The attribute represents a cancellation date.|  
|*DateDuration*|The attribute represents a duration.|  
|*DateEnded*|The attribute represents an end date.|  
|*DateModified*|The attribute represents a modification date.|  
|*DateStart*|The attribute represents a start date.|  
|*DayOfHalfYears*|The attribute represents the day ordinal of a half-year.|  
|*DayOfMonth*|The attribute represents the day ordinal of a month.|  
|*DayOfQuarter*|The attribute represents the day ordinal of a quarter.|  
|*DayOfTrimester*|The attribute represents the day ordinal of a trimester.|  
|*DayOfWeek*|The attribute represents the day ordinal of a week.|  
|*DayOfYear*|The attribute represents the day ordinal of a year.|  
|*Days*|The attribute represents days.|  
|*DaysOfTenDays*|The attribute represents the day ordinal of a ten-day period.|  
|*FiscalDay*|The attribute represents days in a fiscal calendar.|  
|*FiscalDayOfHalfYears*|The attribute represents the day ordinal of a half-year in a fiscal calendar.|  
|*FiscalDayOfMonth*|The attribute represents the day ordinal of a month in a fiscal calendar.|  
|*FiscalDayOfQuarter*|The attribute represents the day ordinal of a quarter in a fiscal calendar.|  
|*FiscalDayOfTrimester*|The attribute represents the day ordinal of a trimester in a fiscal calendar.|  
|*FiscalDayOfWeek*|The attribute represents the day ordinal of a week in a fiscal calendar.|  
|*FiscalDayOfYear*|The attribute represents the day ordinal of a year in a fiscal calendar.|  
|*FiscalHalfYears*|The attribute represents half-years in a fiscal calendar.|  
|*FiscalHalfYearsOfYear*|The attribute represents the half-year ordinal of a year in a fiscal calendar.|  
|*FiscalMonth*|The attribute represents months in a fiscal calendar.|  
|*FiscalMonthOfHalfYears*|The attribute represents the month ordinal of a half-year in a fiscal calendar.|  
|*FiscalMonthOfQuarter*|The attribute represents the month ordinal of a quarter in a fiscal calendar.|  
|*FiscalMonthOfTrimester*|The attribute represents the month ordinal of a trimester in a fiscal calendar.|  
|*FiscalMonthOfYear*|The attribute represents the month ordinal of a year in a fiscal calendar.|  
|*FiscalQuarter*|The attribute represents quarters in a fiscal calendar.|  
|*FiscalQuarterOfHalfYear*|The attribute represents the quarter ordinal of a half-year in a fiscal calendar.|  
|*FiscalQuarterOfYear*|The attribute represents the quarter ordinal of a year in a fiscal calendar.|  
|*FiscalTrimester*|The attribute represents trimesters in a fiscal calendar.|  
|*FiscalTrimesterOfYear*|The attribute represents the trimester ordinal of a year in a fiscal calendar.|  
|*FiscalWeek*|The attribute represents weeks in a fiscal calendar.|  
|*FiscalWeekOfHalfYears*|The attribute represents the week ordinal of a half-year in a fiscal calendar.|  
|*FiscalWeekOfMonth*|The attribute represents the week ordinal of a month in a fiscal calendar.|  
|*FiscalWeekOfQuarter*|The attribute represents the week ordinal of a quarter in a fiscal calendar.|  
|*FiscalWeekOfTrimester*|The attribute represents the week ordinal of a trimester in a fiscal calendar.|  
|*FiscalWeekOfYear*|The attribute represents the week ordinal of a year in a fiscal calendar.|  
|*FiscalYear*|The attribute represents years in a fiscal calendar.|  
|*FormattingColor*|The attribute represents the color used in formatting.|  
|*FormattingFont*|The attribute represents the font used in formatting.|  
|*FormattingFontEffects*|The attribute represents the font effects used in formatting.|  
|*FormattingFontSize*|The attribute represents the font size used in formatting.|  
|*FormattingOrder*|The attribute represents the order used in formatting.|  
|*FormattingSubtotal*|The attribute represents a subtotal.|  
|*GeoBoundaryBottom*|The attribute represents the bottommost value of a geographic boundary.|  
|*GeoBoundaryFront*|The attribute represents the frontmost value of a geographic boundary.|  
|*GeoBoundaryLeft*|The attribute represents the leftmost value of a geographic boundary.|  
|*GeoBoundaryPolygon*|The attribute represents the polygon definition of a geographic boundary.|  
|*GeoBoundaryRear*|The attribute represents the rearmost value of a geographic boundary.|  
|*GeoBoundaryRight*|The attribute represents the rightmost value of a geographic boundary.|  
|*GeoBoundaryTop*|The attribute represents the topmost value of a geographic boundary.|  
|*GeoCentroidX*|The attribute represents an X-axis centroid for a geographic region.|  
|*GeoCentroidY*|The attribute represents a Y-axis centroid for a geographic region.|  
|*GeoCentroidZ*|The attribute represents a Z-axis centroid for a geographic region.|  
|*HalfYears*|The attribute represents half-years.|  
|*HalfYearsOfYear*|The attribute represents the half-year ordinal of a year.|  
|*Hours*|The attribute represents hours.|  
|*Id*|The attribute represents an identifier or key.|  
|*IsHoliday*|The attribute indicates whether a date is a holiday.|  
|*ISO8601DayOfWeek*|The attribute represents the day ordinal of a week in an ISO 8601 calendar.|  
|*ISO8601DayOfYear*|The attribute represents the day ordinal of a year in an ISO 8601 calendar.|  
|*ISO8601Days*|The attribute represents days in an ISO 8601 calendar.|  
|*ISO8601Week*|The attribute represents weeks in an ISO 8601 calendar.|  
|*ISO8601WeekOfYear*|The attribute represents the week ordinal of a year in an ISO 8601 calendar.|  
|*ISO8601Year*|The attribute represents years in an ISO 8601 calendar.|  
|*IsWeekDay*|The attribute indicates whether a date is a weekday.|  
|*IsWorkingDay*|The attribute indicates whether a date is a working day.|  
|*ManufacturingDay*|The attribute represents days in a manufacturing calendar.|  
|*ManufacturingDayOfHalfYears*|The attribute represents the day ordinal of a half-year in a manufacturing calendar.|  
|*ManufacturingDayOfMonth*|The attribute represents the day ordinal of a month in a manufacturing calendar.|  
|*ManufacturingDayOfQuarter*|The attribute represents the day ordinal of a quarter in a manufacturing calendar.|  
|*ManufacturingDayOfTrimester*|The attribute represents the day ordinal of a trimester in a manufacturing calendar.|  
|*ManufacturingDayOfWeek*|The attribute represents the day ordinal of a week in a manufacturing calendar.|  
|*ManufacturingDayOfYear*|The attribute represents the day ordinal of a year in a manufacturing calendar.|  
|*ManufacturingHalfYears*|The attribute represents half-years in a manufacturing calendar.|  
|*ManufacturingHalfYearsOfYear*|The attribute represents the half-year ordinal of a year in a manufacturing calendar.|  
|*ManufacturingMonth*|The attribute represents months in a manufacturing calendar.|  
|*ManufacturingMonthOfHalfYears*|The attribute represents the month ordinal of a half-year in a manufacturing calendar.|  
|*ManufacturingMonthOfQuarter*|The attribute represents the month ordinal of a quarter in a manufacturing calendar.|  
|*ManufacturingMonthOfTrimester*|The attribute represents the month ordinal of a trimester in a manufacturing calendar.|  
|*ManufacturingMonthOfYear*|The attribute represents the month ordinal of a year in a manufacturing calendar.|  
|*ManufacturingQuarter*|The attribute represents quarters in a manufacturing calendar.|  
|*ManufacturingQuarterOfHalfYear*|The attribute represents the quarter ordinal of a half-year in a manufacturing calendar.|  
|*ManufacturingQuarterOfYear*|The attribute represents the quarter ordinal of a year in a manufacturing calendar.|  
|*ManufacturingTrimester*|The attribute represents trimesters in a manufacturing calendar.|  
|*ManufacturingTrimesterOfYear*|The attribute represents the trimester ordinal of a year in a manufacturing calendar.|  
|*ManufacturingWeek*|The attribute represents weeks in a manufacturing calendar.|  
|*ManufacturingWeekOfHalfYears*|The attribute represents the week ordinal of a half-year in a manufacturing calendar.|  
|*ManufacturingWeekOfMonth*|The attribute represents the week ordinal of a month in a manufacturing calendar.|  
|*ManufacturingWeekOfQuarter*|The attribute represents the week ordinal of a quarter in a manufacturing calendar.|  
|*ManufacturingWeekOfTrimester*|The attribute represents the week ordinal of a trimester in a manufacturing calendar.|  
|*ManufacturingWeekOfYear*|The attribute represents the week ordinal of a year in a manufacturing calendar.|  
|*ManufacturingYear*|The attribute represents years in a manufacturing calendar.|  
|*Minutes*|The attribute represents minutes.|  
|*MonthOfHalfYears*|The attribute represents the month ordinal of a half-year.|  
|*MonthOfQuarter*|The attribute represents the month ordinal of a quarter.|  
|*MonthOfTrimester*|The attribute represents the month ordinal of a trimester.|  
|*MonthOfYear*|The attribute represents the month ordinal of a year.|  
|*Months*|The attribute represents months.|  
|*OrganizationalUnit*|The attribute represents an organizational unit.|  
|*OrgTitle*|The attribute represents an organizational title.|  
|*PercentOwnership*|The attribute represents a percent of ownership.|  
|*PercentVoteRight*|The attribute represents a percent of voting rights.|  
|*Person*|The attribute represents a person.|  
|*PersonContact*|The attribute represents contact information for a person.|  
|*PersonDemographic*|The attribute represents demographic information for a person.|  
|*PersonFirstName*|The attribute represents the given name (first name) of a person.|  
|*PersonFullName*|The attribute represents the full name of a person.|  
|*PersonLastName*|The attribute represents the surname (last name) of a person.|  
|*PersonMiddleName*|The attribute represents the middle name of a person.|  
|*PhysicalColor*|The attribute represents a color.|  
|*PhysicalDensity*|The attribute represents density.|  
|*PhysicalDepth*|The attribute represents depth.|  
|*PhysicalHeight*|The attribute represents height.|  
|*PhysicalSize*|The attribute represents a size.|  
|*PhysicalVolume*|The attribute represents volume.|  
|*PhysicalWeight*|The attribute represents weight.|  
|*PhysicalWidth*|The attribute represents width.|  
|*Point*|The attribute represents a point.|  
|*PostalCode*|The attribute represents a postal code.|  
|*Product*|The attribute represents a product.|  
|*ProductBrand*|The attribute represents a product brand.|  
|*ProductCategory*|The attribute represents a product category.|  
|*ProductGroup*|The attribute represents a product group.|  
|*ProductSKU*|The attribute represents a product stock keeping unit (SKU).|  
|*ProjectCode*|The attribute represents a project code.|  
|*Projectcompletion*|The attribute represents the completion status of a project.|  
|*ProjectEnddate*|The attribute represents a project end date.|  
|*ProjectName*|The attribute represents a project name.|  
|*ProjectStartDate*|The attribute represents a project start date.|  
|*Promotion*|The attribute represents a promotion.|  
|*QtyRangeHigh*|The attribute represents the highest value of a range of quantities.|  
|*QtyRangeLow*|The attribute represents the lowest value of a range of quantities.|  
|*Quantitative*|The attribute represents a quantitative attribute.|  
|*QuarterOfHalfYear*|The attribute represents the quarter ordinal of a half-year.|  
|*QuarterOfYear*|The attribute represents the quarter ordinal of a year.|  
|*Quarters*|The attribute represents quarters.|  
|*Rate*|The attribute represents a rate.|  
|*RateType*|The attribute represents a rate type.|  
|*Region*|The attribute represents a customer-defined region.|  
|*Regular*|The attribute represents a regular attribute.|  
|*RelationToParent*|The attribute represents a relation to a parent.|  
|*ReportingDay*|The attribute represents days in a reporting calendar.|  
|*ReportingDayOfHalfYears*|The attribute represents the day ordinal of a half-year in a reporting calendar.|  
|*ReportingDayOfMonth*|The attribute represents the day ordinal of a month in a reporting calendar.|  
|*ReportingDayOfQuarter*|The attribute represents the day ordinal of a quarter in a reporting calendar.|  
|*ReportingDayOfTrimester*|The attribute represents the day ordinal of a trimester in a reporting calendar.|  
|*ReportingDayOfWeek*|The attribute represents the day ordinal of a week in a reporting calendar.|  
|*ReportingDayOfYear*|The attribute represents the day ordinal of a year in a reporting calendar.|  
|*ReportingHalfYears*|The attribute represents half-years in a reporting calendar.|  
|*ReportingHalfYearsOfYear*|The attribute represents the half-year ordinal of a year in a reporting calendar.|  
|*ReportingMonth*|The attribute represents months in a reporting calendar.|  
|*ReportingMonthOfHalfYears*|The attribute represents the month ordinal of a half-year in a reporting calendar.|  
|*ReportingMonthOfQuarter*|The attribute represents the month ordinal of a quarter in a reporting calendar.|  
|*ReportingMonthOfTrimester*|The attribute represents the month ordinal of a trimester in a reporting calendar.|  
|*ReportingMonthOfYear*|The attribute represents the month ordinal of a year in a reporting calendar.|  
|*ReportingQuarter*|The attribute represents quarters in a reporting calendar.|  
|*ReportingQuarterOfHalfYear*|The attribute represents the quarter ordinal of a half-year in a reporting calendar.|  
|*ReportingQuarterOfYear*|The attribute represents the quarter ordinal of a year in a reporting calendar.|  
|*ReportingTrimester*|The attribute represents trimesters in a reporting calendar.|  
|*ReportingTrimesterOfYear*|The attribute represents the trimester ordinal of a year in a reporting calendar.|  
|*ReportingWeek*|The attribute represents weeks in a reporting calendar.|  
|*ReportingWeekOfHalfYears*|The attribute represents the week ordinal of a half-year in a reporting calendar.|  
|*ReportingWeekOfMonth*|The attribute represents the week ordinal of a month in a reporting calendar.|  
|*ReportingWeekOfQuarter*|The attribute represents the week ordinal of a quarter in a reporting calendar.|  
|*ReportingWeekOfTrimester*|The attribute represents the week ordinal of a trimester in a reporting calendar.|  
|*ReportingWeekOfYear*|The attribute represents the week ordinal of a year in a reporting calendar.|  
|*ReportingYear*|The attribute represents years in a reporting calendar.|  
|*Representative*|The attribute represents a representative.|  
|*Scenario*|The attribute represents a scenario.|  
|*Seconds*|The attribute represents seconds.|  
|*Sequence*|The attribute represents a sequence attribute.|  
|*ShortCaption*|The attribute represents a short caption.|  
|*StateOrProvince*|The attribute represents a state or province.|  
|*TenDayOfHalfYears*|The attribute represents the ten-day period ordinal of a half-year.|  
|*TenDayOfQuarter*|The attribute represents the ten-day period ordinal of a quarter.|  
|*TenDayOfTrimester*|The attribute represents the ten-day period ordinal of a trimester.|  
|*TenDayOfYear*|The attribute represents the ten-day period ordinal of a year.|  
|*TenDays*|The attribute represents ten-day periods.|  
|*TenDaysOfMonth*|The attribute represents the ten-day period ordinal of a month.|  
|*Trimester*|The attribute represents trimesters.|  
|*TrimesterOfYear*|The attribute represents the trimester ordinal of a year.|  
|*UndefinedTime*|The attribute represents an undefined time period.|  
|*Utility*|The attribute represents a utility.|  
|*Version*|The attribute represents a version.|  
|*WebHtml*|The attribute represents HTML content.|  
|*WebMailAlias*|The attribute represents an e-mail alias.|  
|*WebUrl*|The attribute represents a URL address.|  
|*WebXmlOrXsl*|The attribute represents XML or XSL content.|  
|*WeekOfHalfYears*|The attribute represents the week ordinal of a half-year.|  
|*WeekOfMonth*|The attribute represents the week ordinal of a month.|  
|*WeekOfQuarter*|The attribute represents the week ordinal of a quarter.|  
|*WeekOfTrimester*|The attribute represents the week ordinal of a trimester.|  
|*WeekOfYear*|The attribute represents the week ordinal of a year.|  
|*Weeks*|The attribute represents weeks.|  
|*Years*|The attribute represents years.|  
  
 The enumeration that corresponds to the allowed values for **Type** in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.AttributeType>.  
  
 The element that corresponds to the parent of **Type** in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.DimensionAttribute>.  
  
## See Also  
 [Attributes Element &#40;ASSL&#41;](../../../analysis-services/scripting/collections/attributes-element-assl.md)   
 [Dimension Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/dimension-element-assl.md)   
 [Properties &#40;ASSL&#41;](../../../analysis-services/scripting/properties/properties-assl.md)  
  
  