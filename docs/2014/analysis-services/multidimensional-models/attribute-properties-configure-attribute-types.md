---
title: "Configure Attribute Types | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "time dimensions [Analysis Services]"
  - "attributes [Analysis Services], types"
  - "slowly changing dimensions"
  - "account dimensions [Analysis Services]"
  - "currency dimensions [Analysis Services]"
  - "Type property"
ms.assetid: c2c6a3da-555e-4362-a83f-88da28427520
author: minewiskan
ms.author: owend
manager: craigg
---
# Configure Attribute Types
  In [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], attribute types help classify an attribute in terms of business functionality. There are many attribute types, most of which are used by client applications to display or support an attribute. However, some attribute types also have specific meaning to [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]. For example, some attribute types identify attributes that represent time periods in various calendars for time dimensions.  
  
##  <a name="setting_attibute_types"></a> Setting Attribute Types  
 The value of the `Type` property for an attribute determines the attribute type for that attribute. Several wizards in [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] set attribute types when defining dimensions or attributes. These [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] wizards also set attribute types when the wizards add functionality to dimensions. For example, the Business Intelligence Wizard applies several attribute types to attributes in a dimension when the wizard adds account intelligence to identify attributes that contain the names, codes, numbers, and structure of accounts in the dimension. The Business Intelligence Wizard also consumes attribute types, such as for currency conversion. For more information, see [Create a Currency type Dimension](database-dimensions-create-a-currency-type-dimension.md).  
  
 The following tables list the attribute types available in [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]. These tables separate attribute types into the following categories:  
  
|Term|Definition|  
|----------|----------------|  
|[General attribute types](#general_attribute_types)|These values are available to all attributes, and exist only to enable classification of attributes for client application purposes.|  
|[Account dimension attribute types](#account_dimension_attribute_types)|These values identify an attribute that belongs to an account dimension. For more information about account dimension, see [Create a Finance Account of parent-child type Dimension](database-dimensions-finance-account-of-parent-child-type.md).|  
|[Currency dimension attribute type](#currency_dimension_attribute_types)|These values identify an attribute that belongs to a currency dimension. For more information about currency dimensions, see [Create a Currency type Dimension](database-dimensions-create-a-currency-type-dimension.md).|  
|[Slowly changing dimension attributes](#slowly_changing_dimension_attribute_types)|These values identify an attribute that belongs to a slowly changing dimension.|  
|[Time dimension attributes](#time_dimension_attribute_types)|These values identify an attribute that belongs to a time dimension. For more information about time dimensions, see [Create a Date type Dimension](database-dimensions-create-a-date-type-dimension.md).|  
  
###  <a name="general_attribute_types"></a> General Attribute Types  
  
|Attribute Type Value|Description|  
|--------------------------|-----------------|  
|`Address`|Represents an address.|  
|`AddressBuilding`|Represents a building identifier for an address.|  
|`AddressCity`|Represents a city for an address.|  
|`AddressCountry`|Represents a country or region for an address.|  
|`AddressFax`|Represents a fax telephone number.|  
|`AddressFloor`|Represents a floor identifier for an address.|  
|`AddressHouse`|Represents a house number for an address.|  
|`AddressPhone`|Represents a telephone number.|  
|`AddressQuarter`|Represents a quarter for an address.|  
|`AddressRoom`|Represents a room identifier for an address.|  
|`AddressStateOrProvince`|Represents a state or province for an address.|  
|`AddressStreet`|Represents the street for an address.|  
|`AddressZip`|Represents a ZIP Code or Postal Code for an address.|  
|`BomResource`|Represents a resource for a bill of materials (BOM).|  
|`Caption`|Represents a caption.|  
|`CaptionAbbreviation`|Represents an abbreviation.|  
|`CaptionDescription`|Represents a description.|  
|`Channel`|Represents a channel.|  
|`City`|Represents a city.|  
|`Company`|Represents a company.|  
|`Continent`|Represents a continent.|  
|`Country`|Represents a country or region.|  
|`County`|Represents a county.|  
|`CustomerGroup`|Represents a group of customers.|  
|`CustomerHousehold`|Represents a household of customers.|  
|`Customers`|Represents a customer.|  
|`DateCanceled`|Represents a cancellation date.|  
|`DateDuration`|Represents a duration.|  
|`DateEnded`|Represents an end date.|  
|`DateModified`|Represents a modification date.|  
|`DateStart`|Represents a start date.|  
|**DeletedFlag**|Indicates whether a member is or should be deleted (in terms of business functionality.)<br /><br /> Note: [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] does not use this attribute type to determine whether a member should be deleted. Instead, this attribute type is intended to be used by client applications for display purposes only.|  
|`FormattingColor`|Represents the color used in formatting.|  
|`FormattingFont`|Represents the font used in formatting.|  
|`FormattingFontEffects`|Represents the font effects used in formatting.|  
|`FormattingFontSize`|Represents the font size used in formatting.|  
|`FormattingOrder`|Represents the order used in formatting.|  
|`FormattingSubtotal`|Represents a subtotal.|  
|`GeoBoundaryBottom`|Represents the bottommost value of a geographic boundary.|  
|`GeoBoundaryFront`|Represents the value at the front of a geographic boundary.|  
|`GeoBoundaryLeft`|Represents the leftmost value of a geographic boundary.|  
|`GeoBoundaryPolygon`|Represents the polygon definition of a geographic boundary.|  
|`GeoBoundaryRear`|Represents the rearmost value of a geographic boundary.|  
|`GeoBoundaryRight`|Represents the rightmost value of a geographic boundary.|  
|`GeoBoundaryTop`|Represents the topmost value of a geographic boundary.|  
|`GeoCentroidX`|Represents an X-axis centroid for a geographic region.|  
|`GeoCentroidY`|Represents a Y-axis centroid for a geographic region.|  
|`GeoCentroidZ`|Represents a Z-axis centroid for a geographic region.|  
|`ID`|Represents an identifier (ID) or key.|  
|`Image`|Represents an image in an undefined graphic format.|  
|`ImageBmp`|Represents an image in bitmap graphic format.|  
|`ImageGif`|Represents an image in Graphics Interchange Format (GIF) graphic format.|  
|`ImageJpg`|Represents an image in Joint Photographic Experts Group (JPEG) graphic format.|  
|`ImagePng`|Represents an image in Portable Network Graphics (PNG) graphic format.|  
|`ImageTiff`|Represents an image in Tagged Image File Format (TIFF) graphic format.|  
|`OrganizationalUnit`|Represents an organizational unit.|  
|`OrgTitle`|Represents an organizational title.|  
|`PercentOwnership`|Represents a percent of ownership.|  
|`PercentVoteRight`|Represents a percent of voting rights.|  
|`Person`|Represents a person.|  
|`PersonContact`|Represents contact information for a person.|  
|`PersonDemographic`|Represents demographic information for a person.|  
|`PersonFirstName`|Represents the first name of a person.|  
|`PersonFullName`|Represents the full name of a person.|  
|`PersonLastName`|Represents the surname (last name) of a person.|  
|`PersonMiddleName`|Represents the middle name of a person.|  
|`PhysicalColor`|Represents a color.|  
|`PhysicalDensity`|Represents density.|  
|`PhysicalDepth`|Represents depth.|  
|`PhysicalHeight`|Represents height.|  
|`PhysicalSize`|Represents a size.|  
|`PhysicalVolume`|Represents volume.|  
|`PhysicalWeight`|Represents weight.|  
|`PhysicalWidth`|Represents width.|  
|`Point`|Represents a point.|  
|`PostalCode`|Represents a postal code.|  
|`Product`|Represents a product.|  
|`ProductBrand`|Represents a product brand.|  
|`ProductCategory`|Represents a product category.|  
|`ProductGroup`|Represents a product group.|  
|`ProductSKU`|Represents a product stock keeping unit (SKU).|  
|`Project`|Represents a project.|  
|`ProjectCode`|Represents a project code.|  
|`ProjectCompletion`|Represents the completion status of a project.|  
|`ProjectEndDate`|Represents a project end date.|  
|`ProjectName`|Represents a project name.|  
|`ProjectStartDate`|Represents a project start date.|  
|`Promotion`|Represents a promotion.|  
|`QtyRangeHigh`|Represents the highest value of a range of quantities.|  
|`QtyRangeLow`|Represents the lowest value of a range of quantities.|  
|`Quantitative`|Represents a quantitative attribute.|  
|`Rate`|Represents a rate.|  
|`RateType`|Represents a rate type.|  
|`Region`|Represents a customer-defined region.|  
|`Regular`|Represents a regular attribute.|  
|`RelationToParent`|Represents a relation to a parent.|  
|`Representative`|Represents a representative.|  
|`Scenario`|Represents a scenario.|  
|`Sequence`|Represents a sequence attribute.|  
|`ShortCaption`|Represents a short caption.|  
|`StateOrProvince`|Represents a state or province.|  
|`Utility`|Represents a utility.|  
|`Version`|Represents a version.|  
|`WebHtml`|Represents HTML content.|  
|`WebMailAlias`|Represents an e-mail alias.|  
|`WebUrl`|Represents a URL address.|  
|`WebXmlOrXsl`|Represents XML or XSL content.|  
  
###  <a name="account_dimension_attribute_types"></a> Account Dimension Attribute Types  
  
|Attribute Type Value|Description|  
|--------------------------|-----------------|  
|`Account`|Represents the parent of an account. This attribute type is typically applied to the parent attribute of an account dimension.|  
|`AccountName`|Represents the name of an account. This attribute type is typically applied to the key attributes of an account dimension.|  
|`AccountNumber`|Represents the number of an account.|  
|`AccountType`|Represents the type of an account. This attribute type identifies the aggregation function of an account member in an account type dimension in the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database.|  
  
###  <a name="currency_dimension_attribute_types"></a> Currency Dimension Attribute Types  
  
|Attribute Type Value|Description|  
|--------------------------|-----------------|  
|`CurrencyDestination`|Represents the destination currency of a currency exchange. This attribute type is typically applied to the key attribute of a reporting dimension, for use in currency conversion. For more information about currency conversion, see [Currency Conversions &#40;Analysis Services&#41;](../currency-conversions-analysis-services.md).|  
|`CurrencyIsoCode`|Represents the International Standards Organization (ISO) code of a currency. For more information about currency conversion, see [Currency Conversions &#40;Analysis Services&#41;](../currency-conversions-analysis-services.md).|  
|`CurrencyName`|Represents the name of a currency. For more information about currency conversion, see [Currency Conversions &#40;Analysis Services&#41;](../currency-conversions-analysis-services.md).|  
|`CurrencySource`|Represents the source currency of a currency exchange. This attribute type is typically applied to the key attribute of a currency dimension, for use in currency conversion. For more information about currency conversion, see [Currency Conversions &#40;Analysis Services&#41;](../currency-conversions-analysis-services.md).|  
  
###  <a name="slowly_changing_dimension_attribute_types"></a> Slowly Changing Dimension Attribute Types  
  
|Attribute Type Value|Description|  
|--------------------------|-----------------|  
|**ScdEndDate**|Represents the effective end date for a member in a slowly changing dimension.|  
|**ScdOriginalID**|Represents the original identifier for a member in a slowly changing dimension.|  
|**ScdStartDate**|Represents the effective start date for a member in a slowly changing dimension.|  
|`ScdStatus`|Represents the effective status of a member in a slowly changing dimension.|  
  
###  <a name="time_dimension_attribute_types"></a> Time Dimension Attribute Types  
  
|Attribute Type Value|Description|  
|--------------------------|-----------------|  
|`Date`|Represents a date. This attribute type is typically applied to the key attribute of a time dimension or server time dimension.|  
|`DayOfHalfYear`|Represents the day ordinal of a half-year.|  
|`DayOfMonth`|Represents the day ordinal of a month.|  
|`DayOfQuarter`|Represents the day ordinal of a quarter.|  
|`DayOfTenDays`|Represents the day ordinal of a ten-day period.|  
|`DayOfTrimester`|Represents the day ordinal of a trimester.|  
|`DayOfWeek`|Represents the day ordinal of a week.|  
|`DayOfYear`|Represents the day ordinal of a year.|  
|`Days`|Represents days.|  
|`FiscalDate`|Represents a date in a fiscal calendar.|  
|`FiscalDayOfHalfYear`|Represents the day ordinal of a half-year in a fiscal calendar.|  
|`FiscalDayOfMonth`|Represents the day ordinal of a month in a fiscal calendar.|  
|`FiscalDayOfQuarter`|Represents the day ordinal of a quarter in a fiscal calendar.|  
|`FiscalDayOfTrimester`|Represents the day ordinal of a trimester in a fiscal calendar.|  
|`FiscalDayOfWeek`|Represents the day ordinal of a week in a fiscal calendar.|  
|`FiscalDayOfYear`|Represents the day ordinal of a year in a fiscal calendar.|  
|`FiscalHalfYears`|Represents half-years in a fiscal calendar.|  
|`FiscalHalfYearOfYear`|Represents the half-year ordinal of a year in a fiscal calendar.|  
|`FiscalMonths`|Represents months in a fiscal calendar.|  
|`FiscalMonthOfHalfYear`|Represents the month ordinal of a half-year in a fiscal calendar.|  
|`FiscalMonthOfQuarter`|Represents the month ordinal of a quarter in a fiscal calendar.|  
|`FiscalMonthOfTrimester`|Represents the month ordinal of a trimester in a fiscal calendar.|  
|`FiscalMonthOfYear`|Represents the month ordinal of a year in a fiscal calendar.|  
|`FiscalQuarters`|Represents quarters in a fiscal calendar.|  
|`FiscalQuarterOfHalfYear`|Represents the quarter ordinal of a half-year in a fiscal calendar.|  
|`FiscalQuarterOfYear`|Represents the quarter ordinal of a year in a fiscal calendar.|  
|`FiscalTrimesters`|Represents trimesters in a fiscal calendar.|  
|`FiscalTrimesterOfYear`|Represents the trimester ordinal of a year in a fiscal calendar.|  
|`FiscalWeeks`|Represents weeks in a fiscal calendar.|  
|`FiscalWeekOfHalfYear`|Represents the week ordinal of a half-year in a fiscal calendar.|  
|`FiscalWeekOfMonth`|Represents the week ordinal of a month in a fiscal calendar.|  
|`FiscalWeekOfQuarter`|Represents the week ordinal of a quarter in a fiscal calendar.|  
|`FiscalWeekOfTrimester`|Represents the week ordinal of a trimester in a fiscal calendar.|  
|`FiscalWeekOfYear`|Represents the week ordinal of a year in a fiscal calendar.|  
|`FiscalYears`|Represents years in a fiscal calendar.|  
|`HalfYears`|Represents half-years.|  
|`HalfYearOfYear`|Represents the half-year ordinal of a year.|  
|`Hours`|Represents hours.|  
|`IsHoliday`|Indicates whether a date is a holiday.|  
|`ISO8601Date`|Represents a date in an ISO 8601 calendar.|  
|`ISO8601DayOfWeek`|Represents the day ordinal of a week in an ISO 8601 calendar.|  
|`ISO8601DayOfYear`|Represents the day ordinal of a year in an ISO 8601 calendar.|  
|`ISO8601Weeks`|Represents weeks in an ISO 8601 calendar.|  
|`ISO8601WeekOfYear`|Represents the week ordinal of a year in an ISO 8601 calendar.|  
|`ISO8601Years`|Represents years in an ISO 8601 calendar.|  
|`IsPeakDay`|Indicates whether a date is a peak day.|  
|`IsWeekDay`|Indicates whether a date is a weekday.|  
|`IsWorkingDay`|Indicates whether a date is a working day.|  
|`ManufacturingDate`|Represents a date in a manufacturing calendar.|  
|`ManufacturingDayOfHalfYear`|Represents the day ordinal of a half-year in a manufacturing calendar.|  
|`ManufacturingDayOfMonth`|Represents the day ordinal of a month in a manufacturing calendar.|  
|`ManufacturingDayOfQuarter`|Represents the day ordinal of a quarter in a manufacturing calendar.|  
|`ManufacturingDayOfTrimester`|Represents the day ordinal of a trimester in a manufacturing calendar.|  
|`ManufacturingDayOfWeek`|Represents the day ordinal of a week in a manufacturing calendar.|  
|`ManufacturingDayOfYear`|Represents the day ordinal of a year in a manufacturing calendar.|  
|`ManufacturingHalfYears`|Represents half-years in a manufacturing calendar.|  
|`ManufacturingHalfYearOfYear`|Represents the half-year ordinal of a year in a manufacturing calendar.|  
|`ManufacturingMonths`|Represents months in a manufacturing calendar.|  
|`ManufacturingMonthOfHalfYear`|Represents the month ordinal of a half-year in a manufacturing calendar.|  
|`ManufacturingMonthOfQuarter`|Represents the month ordinal of a quarter in a manufacturing calendar.|  
|`ManufacturingMonthOfTrimester`|Represents the month ordinal of a trimester in a manufacturing calendar.|  
|`ManufacturingMonthOfYear`|Represents the month ordinal of a year in a manufacturing calendar.|  
|`ManufacturingQuarters`|Represents quarters in a manufacturing calendar.|  
|`ManufacturingQuarterOfHalfYear`|Represents the quarter ordinal of a half-year in a manufacturing calendar.|  
|`ManufacturingQuarterOfYear`|Represents the quarter ordinal of a year in a manufacturing calendar.|  
|`ManufacturingWeeks`|Represents weeks in a manufacturing calendar.|  
|`ManufacturingWeekOfHalfYear`|Represents the week ordinal of a half-year in a manufacturing calendar.|  
|`ManufacturingWeekOfMonth`|Represents the week ordinal of a month in a manufacturing calendar.|  
|`ManufacturingWeekOfQuarter`|Represents the week ordinal of a quarter in a manufacturing calendar.|  
|`ManufacturingWeekOfTrimester`|Represents the week ordinal of a trimester in a manufacturing calendar.|  
|`ManufacturingWeekOfYear`|Represents the week ordinal of a year in a manufacturing calendar.|  
|`ManufacturingYears`|Represents years in a manufacturing calendar.|  
|`Minutes`|Represents minutes.|  
|`Months`|Represents months.|  
|`MonthOfHalfYear`|Represents the month ordinal of a half-year.|  
|`MonthOfQuarter`|Represents the month ordinal of a quarter.|  
|`MonthOfTrimester`|Represents the month ordinal of a trimester.|  
|`MonthOfYear`|Represents the month ordinal of a year.|  
|`Quarters`|Represents quarters.|  
|`QuarterOfHalfYear`|Represents the quarter ordinal of a half-year.|  
|`QuarterOfYear`|Represents the quarter ordinal of a year.|  
|`ReportingDate`|Represents a date in a reporting calendar.|  
|`ReportingDayOfHalfYear`|Represents the day ordinal of a half-year in a reporting calendar.|  
|`ReportingDayOfMonth`|Represents the day ordinal of a month in a reporting calendar.|  
|`ReportingDayOfQuarter`|Represents the day ordinal of a quarter in a reporting calendar.|  
|`ReportingDayOfTrimester`|Represents the day ordinal of a trimester in a reporting calendar.|  
|`ReportingDayOfWeek`|Represents the day ordinal of a week in a reporting calendar.|  
|`ReportingDayOfYear`|Represents the day ordinal of a year in a reporting calendar.|  
|`ReportingHalfYears`|Represents half-years in a reporting calendar.|  
|`ReportingHalfYearOfYear`|Represents the half-year ordinal of a year in a reporting calendar.|  
|`ReportingMonths`|Represents months in a reporting calendar.|  
|`ReportingMonthOfHalfYear`|Represents the month ordinal of a half-year in a reporting calendar.|  
|`ReportingMonthOfQuarter`|Represents the month ordinal of a quarter in a reporting calendar.|  
|`ReportingMonthOfTrimester`|Represents the month ordinal of a trimester in a reporting calendar.|  
|`ReportingMonthOfYear`|Represents the month ordinal of a year in a reporting calendar.|  
|`ReportingQuarters`|Represents quarters in a reporting calendar.|  
|`ReportingQuarterOfHalfYear`|Represents the quarter ordinal of a half-year in a reporting calendar.|  
|`ReportingQuarterOfYear`|Represents the quarter ordinal of a year in a reporting calendar.|  
|`ReportingTrimesters`|Represents trimesters in a reporting calendar.|  
|`ReportingTrimesterOfYear`|Represents the trimester ordinal of a year in a reporting calendar.|  
|`ReportingWeeks`|Represents weeks in a reporting calendar.|  
|`ReportingWeekOfHalfYear`|Represents the week ordinal of a half-year in a reporting calendar.|  
|`ReportingWeekOfMonth`|Represents the week ordinal of a month in a reporting calendar.|  
|`ReportingWeekOfQuarter`|Represents the week ordinal of a quarter in a reporting calendar.|  
|`ReportingWeekOfTrimester`|Represents the week ordinal of a trimester in a reporting calendar.|  
|`ReportingWeekOfYear`|Represents the week ordinal of a year in a reporting calendar.|  
|`ReportingYears`|Represents years in a reporting calendar.|  
|`Seconds`|Represents seconds.|  
|`TenDayOfHalfYear`|Represents the ten-day period ordinal of a half-year.|  
|`TenDayOfMonth`|Represents the ten-day period ordinal of a month.|  
|`TenDayOfQuarter`|Represents the ten-day period ordinal of a quarter.|  
|`TenDayOfTrimester`|Represents the ten-day period ordinal of a trimester.|  
|`TenDayOfYear`|Represents the ten-day period ordinal of a year.|  
|`TenDays`|Represents ten-day periods.|  
|`Trimesters`|Represents trimesters.|  
|`TrimesterOfYear`|Represents the trimester ordinal of a year.|  
|`UndefinedTime`|Represents an undefined time period.|  
|`WeekOfYear`|Represents the week ordinal of a year.|  
|`Weeks`|Represents weeks.|  
|**WinterSummerSeason**|Indicates whether the date is part of the winter/summer season.|  
|`Years`|Represents years.|  
  
## See Also  
 [Attributes and Attribute Hierarchies](../multidimensional-models-olap-logical-dimension-objects/attributes-and-attribute-hierarchies.md)   
 [Dimension Attribute Properties Reference](dimension-attribute-properties-reference.md)  
  
  
