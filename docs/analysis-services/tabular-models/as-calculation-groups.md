---
title: "Calculation groups in Analysis Services tabular models | Microsoft Docs"
ms.date: 06/05/2019
ms.prod: sql
ms.technology: analysis-services
ms.custom: tabular-models
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
monikerRange: ">=sql-server-ver15||=sqlallproducts-allversions"
---
# Calculation groups (Preview)
 
[!INCLUDE[ssas-appliesto-sql2019](../../includes/ssas-appliesto-sql2019.md)]

Calculation groups can significantly reduce the number of redundant measures by grouping common measure expressions as *calculation items*. Calculation groups are supported in SQL Server Analysis Services 2019 tabular models at the 1470 and higher [compatibility level](compatibility-level-for-tabular-models-in-analysis-services.md). Models at the 1470 compatibility level are currently in **Preview**.  

This article describes: 

> [!div class="checklist"]
> * Benefits 
> * How calculation groups work
> * Dynamic format strings
> * Precedence
> * Tools
> * Limitations



## Benefits

Calculation groups address an issue in complex models where there can be a proliferation of redundant measures using the same calculations - most common with time-intelligence calculations. For example, a sales analyst wants to view sales totals and orders by month-to-date (MTD), quarter-to-date (QTD), year-to-date (YTD), orders year-to-date for the previous year (PY), and so on. The data modeler has to create separate measures for each calculation, which can lead to dozens of measures. For the user, this can mean having to sort through just as many measures, and apply them individually to their report. 

Let's first take a look at how calculation groups appear to users in a reporting tool like Power BI. We'll then take a look at what makes up a calculation group, and how they're created in a model.

Calculation groups are shown in reporting clients as a table with a single column. The column isn't like a typical column or dimension, instead it represents one or more reusable calculations, or *calculation items* that can be applied to any measure already added to the Values filter for a visualization.

In the following animation, a user is analyzing sales data for years 2012 and 2013. Before applying a calculation group, the common base measure **Sales**  calculates a sum of total sales for each month. The user then wants to apply time-intelligence calculations to get sales totals for month to date, quarter to date, year to date, and so on. Without calculation groups, the user would have to select individual time-intelligence measures.

With a calculation group, in this example named **Time Intelligence**, when the user drags the **Time Calculation** item to the **Columns** filter area, each calculation item appears as a separate column. Values for each row are calculated from the base measure, **Sales**.  

![Calculation group being applied in Power BI](media/as-calculation-groups/as-calc-groups-pbi.gif)


Calculation groups work with **explicit** DAX measures. In this example, **Sales** is an explicit measure already created in the model. Calculation groups do not work with implicit DAX measures. For example, in Power BI implicit measures are created when a user drags columns onto visuals to view aggregated values, without creating an explicit measure. At this time, Power BI generates DAX for implicit measures written as inline DAX calculations - meaning implicit measures cannot work with calculation groups. A new model property visible in the Tabular Object Model (TOM) has been introduced, **DiscourageImplicitMeasures**. Currently, in order to create calculation groups this property must be set to **true**. When set to true, Power BI Desktop in Live Connect mode disables creation of implicit measures.

## How they work

Now that you've seen how calculation groups benefit users, let's take a look at how the Time Intelligence calculation group example shown is created.

Before we go into the details, let's introduce some new DAX functions specifically for calculation groups: 

[SELECTEDMEASURE](https://docs.microsoft.com/dax/selectedmeasure-function-dax) - Used by expressions for calculation items to reference the measure that is currently in context. In this example, the Sales measure.

[SELECTEDMEASURENAME](https://docs.microsoft.com/dax/selectedmeasurename-function-dax) - Used by expressions for calculation items to determine the measure that is in context by name.

[ISSELECTEDMEASURE](https://docs.microsoft.com/dax/isselectedmeasure-function-dax) - Used by expressions for calculation items to determine the measure that is in context is specified in a list of measures.

[SELECTEDMEASUREFORMATSTRING](https://docs.microsoft.com/dax/selectedmeasurefromatstring-function-dax) - Used by expressions for calculation items to retrieve the format string of the measure that is in context.

### Time Intelligence example

Table name - **Time Intelligence**   
Column name - **Time Calculation**   
Precedence - **20**   

#### Time Intelligence calculation items

**Current**

```dax
SELECTEDMEASURE()
```

**MTD**

```dax
CALCULATE(SELECTEDMEASURE(), DATESMTD(DimDate[Date]))
```

**QTD**

```dax
CALCULATE(SELECTEDMEASURE(), DATESQTD(DimDate[Date]))
```

**YTD**

```dax
CALCULATE(SELECTEDMEASURE(), DATESYTD(DimDate[Date]))
```

**PY**

```dax
CALCULATE(SELECTEDMEASURE(), SAMEPERIODLASTYEAR(DimDate[Date]))
```

**PY MTD**

```dax
CALCULATE(
    SELECTEDMEASURE(),
    SAMEPERIODLASTYEAR(DimDate[Date]),
    'Time Intelligence'[Time Calculation] = "MTD"
)
```

**PY QTD**

```dax
CALCULATE(
    SELECTEDMEASURE(),
    SAMEPERIODLASTYEAR(DimDate[Date]),
    'Time Intelligence'[Time Calculation] = "QTD"
)
```

**PY YTD**

```dax
CALCULATE(
    SELECTEDMEASURE(),
    SAMEPERIODLASTYEAR(DimDate[Date]),
    'Time Intelligence'[Time Calculation] = "YTD"
)
```

**YOY**

```dax
SELECTEDMEASURE() –
CALCULATE(
    SELECTEDMEASURE(),
    'Time Intelligence'[Time Calculation] = "PY"
)
```

**YOY%**

```dax
DIVIDE(
    CALCULATE(
        SELECTEDMEASURE(),
        'Time Intelligence'[Time Calculation]="YOY"
    ),
    CALCULATE(
        SELECTEDMEASURE(),
        'Time Intelligence'[Time Calculation]="PY"
    ),
)
```

To test this calculation group, you can execute a DAX query in SSMS or the open-source [DAX Studio](http://daxstudio.org/). Note: YOY and YOY% are omitted from this query example.

#### Time Intelligence query

```dax
EVALUATE
CALCULATETABLE (
    SUMMARIZECOLUMNS (
        DimDate[CalendarYear],
        DimDate[EnglishMonthName],
        "Current", CALCULATE ( [Sales], 'Time Intelligence'[Time Calculation] = "Current" ),
        "QTD",     CALCULATE ( [Sales], 'Time Intelligence'[Time Calculation] = "QTD" ),
        "YTD",     CALCULATE ( [Sales], 'Time Intelligence'[Time Calculation] = "YTD" ),
        "PY",      CALCULATE ( [Sales], 'Time Intelligence'[Time Calculation] = "PY" ),
        "PY QTD",  CALCULATE ( [Sales], 'Time Intelligence'[Time Calculation] = "PY QTD" ),
        "PY YTD",  CALCULATE ( [Sales], 'Time Intelligence'[Time Calculation] = "PY YTD" )
    ),
    DimDate[CalendarYear] IN { 2012, 2013 }
)
```

#### Time Intelligence query return

The return table shows calculations for each calculation item applied. For example, you can see QTD for March 2012 is the sum of January, February and March 2012.

![Query return](media/as-calculation-groups/as-calc-groups-query-return.png)


## Dynamic format strings

*Dynamic format strings* with calculation groups allow conditional application of format strings to measures without forcing them to return strings.

Tabular models support dynamic formatting of measures by using DAX's [FORMAT](https://docs.microsoft.com/dax/format-function-dax) function. However, the FORMAT function has the disadvantage of returning a string, forcing measures that would otherwise be numeric to also be returned as a string. This can have some limitations, such as not working with most Power BI visuals depending on numeric values, like charts.

### Dynamic format strings for time-intelligence

If we look at the Time Intelligence example shown above, all the calculation items except **YOY%** should use the format of the current measure in context. For example, **YTD** calculated on the Sales base measure should be currency. If this were a calculation group for something like an Orders base measure, the format would be numeric. **YOY%**, however, should be a percentage regardless of the format of the base measure.

For **YOY%**, we can override the format string by setting the format string expression property to **0.00%;-0.00%;0.00%**. To learn more about format string expression properties, see [MDX Cell Properties - FORMAT STRING  Contents](../multidimensional-models/mdx/mdx-cell-properties-format-string-contents.md#numeric-values).

In this matrix visual in Power BI, you see **Sales Current/YOY** and **Orders Current/YOY** retain their respective base measure format strings. **Sales YOY%** and **Orders YOY%**, however, overrides the format string to use *percentage* format.

![Time intelligence in matrix visual](media/as-calculation-groups/as-calc-groups-dynamicstring-timeintel.png)

### Dynamic format strings for currency conversion

Dynamic format strings provide easy currency conversion. Consider the following Adventure Works data model. It's modeled for *one-to-many* currency conversion as defined by  [Conversion types](../currency-conversions-analysis-services.md#conversion-types).

![Currency rate in tabular model](media/as-calculation-groups/as-calc-groups-currency-conversion.png)

A **FormatString** column is added to the **DimCurrency** table and populated with format strings for the respective currencies.

![Format string column](media/as-calculation-groups/as-calc-groups-formatstringcolumn.png)

For this example, the following calculation group is then defined as:

### Currency Conversion example

Table name - **Currency Conversion**   
Column name - **Conversion Calculation**   
Precedence - **5**   

#### Calculation items for Currency Conversion

**No Conversion**

```dax
SELECTEDMEASURE()
```

**Converted Currency**

```dax
IF(
    //Check one currency in context & not US Dollar, which is the pivot currency:
    SELECTEDVALUE( DimCurrency[CurrencyName], "US Dollar" ) = "US Dollar",
    SELECTEDMEASURE(),
    SUMX(
        VALUES(DimDate[Date]),
        CALCULATE( DIVIDE( SELECTEDMEASURE(), MAX(FactCurrencyRate[EndOfDayRate]) ) )
    )
)
```

Format string expression

```dax
SELECTEDVALUE(
    DimCurrency[FormatString],
    SELECTEDMEASUREFORMATSTRING()
)
```
The format string expression must return a scalar string. It uses the new [SELECTEDMEASUREFORMATSTRING](https://docs.microsoft.com/dax/selectedmeasurefromatstring-function-dax) function to revert to the base measure format string if there are multiple currencies in filter context.

The following animation shows the dynamic format currency conversion of the **Sales** measure in a report.

![Currency conversion dynamic format string applied](media/as-calculation-groups/as-calc-groups-dynamic-format-string.gif)

## Precedence

Precedence is a property defined for a calculation group. It specifies the order of evaluation when there is more than one calculation group. A higher number indicates greater precedence, meaning it will be evaluated before calculation groups with lower precedence.

For this example, we'll use same model as the time-intelligence example above, but also add an **Averages** calculation group. The Averages calculation group contains average calculations that are independent of traditional time intelligence in that they don’t change the date filter context - they just apply average calculations within it.

In this example, a daily average calculation is defined. Calculations such as average barrels of oil per day are common in oil-and-gas applications. Other common business examples include store sales average in retail.

While such calculations are calculated independently of time-intelligence calculations, there may well be a requirement to combine them. For example, a user might want to see barrels of oil per day YTD to view the daily oil rate from the beginning of the year to the current date. In this scenario, precedence should be set for calculation items.

### Averages example

Table name is **Averages**.   
Column name is **Average Calculation**.   
Precedence is **10**.   

#### Calculation items for Averages

**No Average**

```dax
SELECTEDMEASURE()
```

**Daily Average**

```dax
DIVIDE(SELECTEDMEASURE(), COUNTROWS(DimDate))
```

Here's an example of a DAX query and return table:

#### Averages query

```dax
EVALUATE
    CALCULATETABLE (
        SUMMARIZECOLUMNS (
        DimDate[CalendarYear],
        DimDate[EnglishMonthName],
        "Sales", CALCULATE (
            [Sales],
            'Time Intelligence'[Time Calculation] = "Current",
            'Averages'[Average Calculation] = "No Average"
        ),
        "YTD", CALCULATE (
            [Sales],
            'Time Intelligence'[Time Calculation] = "YTD",
            'Averages'[Average Calculation] = "No Average"
        ),
        "Daily Average", CALCULATE (
            [Sales],
            'Time Intelligence'[Time Calculation] = "Current",
            'Averages'[Average Calculation] = "Daily Average"
        ),
        "YTD Daily Average", CALCULATE (
            [Sales],
            'Time Intelligence'[Time Calculation] = "YTD",
            'Averages'[Average Calculation] = "Daily Average"
        )
    ),
    DimDate[CalendarYear] = 2012
)
```

#### Averages query return

![Query return](media/as-calculation-groups/as-calc-groups-ytd-daily-avg.png)

The following table shows how the March 2012 values are calculated.


|Column name  |Calculation |
|---------|---------|
|YTD     |    Sum of Sales for Jan, Feb, Mar 2012<br />= 495,364 + 506,994 + 373,483     |
|Daily Average    |  	Sales for Mar 2012 divided by # of days in March<br />= 373,483 / 31       |
|YTD Daily Average     | YTD for Mar 2012 divided by # of days in Jan, Feb, and Mar<br />=  1,375,841 / (31 + 29 + 31)       |

Here's the definition of the YTD calculation item, applied with precedence of **20**.

```dax
CALCULATE(SELECTEDMEASURE(), DATESYTD(DimDate[Date]))
```

Here's Daily Average, applied with a precedence of **10**.

```dax
DIVIDE(SELECTEDMEASURE(), COUNTROWS(DimDate))
```

Since the precedence of the Time Intelligence calculation group is higher than that of the Averages calculation group, it's applied as broadly as possible. The YTD Daily Average calculation applies YTD to both the numerator and the denominator (count of days) of the daily average calculation.

This is equivalent to the following expression:

```dax
CALCULATE(DIVIDE(SELECTEDMEASURE(), COUNTROWS(DimDate)), DATESYTD(DimDate[Date]))
```

Not this expression:

```dax
DIVIDE(CALCULATE(SELECTEDMEASURE(), DATESYTD(DimDate[Date])), COUNTROWS(DimDate)))
```

## Sideways recursion

In the Time Intelligence example above, some of the calculation items refer to others in the same calculation group. This is called *sideways recursion*. For example, **YOY%** references both **YOY** and **PY**.

```dax
DIVIDE(
    CALCULATE(
        SELECTEDMEASURE(),
        'Time Intelligence'[Time Calculation]="YOY"
    ),
    CALCULATE(
        SELECTEDMEASURE(),
        'Time Intelligence'[Time Calculation]="PY"
    ),
)
```

In this case, both expressions are evaluated separately because they are using different calculate statements. Other types of recursion are not supported.

## Single calculation item in filter context

In our Time Intelligence example, the **PY YTD** calculation item has a single calculate expression:

```dax
CALCULATE(
    SELECTEDMEASURE(),
    SAMEPERIODLASTYEAR(DimDate[Date]),
    'Time Intelligence'[Time Calculation] = "YTD"
)
```

The YTD argument to the CALCULATE() function overrides the filter context to reuse the logic already defined in the YTD calculation item. It's not possible to apply both PY and YTD in a single evaluation. Calculation groups are *only applied* if a single calculation item from the calculation group is in filter context.

## MDX support

Calculation groups support Multidimensional Data Expressions (MDX) queries. This means, Microsoft Excel users, which query tabular data models by using MDX, can take full advantage of calculation groups in worksheet PivotTables and charts.

## Tools

Calculation groups  are not yet supported in SQL Server Data Tools, Visual Studio with Analysis Services extensions. However, calculation groups can be created by using Tabular Model Scripting Language (TMSL) or the open source [Tabular Editor](https://github.com/otykier/TabularEditor).

## Limitations

[Object level security](object-level-security.md) (OLS) defined on calculation group tables is not supported. However, OLS can be defined on other tables in the same model. If a calculation item refers to an OLS secured object, a generic error is returned.

[Row level security](roles-ssas-tabular.md#bkmk_rowfliters) (RLS) is not supported. You can define RLS on tables in the same model, but not on calculation groups themselves (directly or indirectly).

[Detail Rows Expressions](../tutorial-tabular-1400/as-supplemental-lesson-detail-rows.md) are not supported with calculation groups.

## See also  

[DAX in tabular models](understanding-dax-in-tabular-models-ssas-tabular.md)   
[DAX Reference](https://docs.microsoft.com/dax/data-analysis-expressions-dax-reference)  
