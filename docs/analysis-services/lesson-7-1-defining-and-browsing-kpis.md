---
title: "Defining and Browsing KPIs | Microsoft Docs"
ms.date: 05/08/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: multidimensional-models
ms.topic: tutorial
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Lesson 7-1 - Defining and Browsing KPIs
[!INCLUDE[ssas-appliesto-sqlas](../includes/ssas-appliesto-sqlas.md)]

To define key performance indicators (KPIs), you first define a KPI name and the measure group to which the KPI is associated. A KPI can be associated with all measure groups or with a single measure group. You then define the following elements of the KPI:  
  
-   The value expression  
  
    A value expression is a physical measure such as Sales, a calculated measure such as Profit, or a calculation that is defined within the KPI by using a Multidimensional Expressions (MDX) expression.  
  
-   The goal expression  
  
    A goal expression is a value, or an MDX expression that resolves to a value, that defines the target for the measure that the value expression defines. For example, a goal expression could be the amount by which the business managers of a company want to increase sales or profit.  
  
-   The status expression  
  
    A status expression is an MDX expression that [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] uses to evaluate the current status of the value expression compared to the goal expression. A goal expression is a normalized value in the range of -1 to +1, where -1 is very bad, and +1 is very good. The status expression displays a graphic to help you easily determine the status of the value expression compared to the goal expression.  
  
-   The trend expression  
  
    A trend expression is an MDX expression that [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] uses to evaluate the current trend of the value expression compared to the goal expression. The trend expression helps the business user to quickly determine whether the value expression is becoming better or worse relative to the goal expression. You can associate one of several graphics with the trend expression to help business users be able to quickly understand the trend.  
  
In addition to these elements that you define for a KPI, you also define several properties of a KPI. These properties include a display folder, a parent KPI if the KPI is computed from other KPIs, the current time member if there is one, the weight of the KPI if it has one, and a description of the KPI.  
  
> [!NOTE]  
> For more examples of KPIs, see the KPI examples on the Templates tab in the Calculation Tools pane or in the examples in the **Adventure Works DW 2012** sample data warehouse. For more information about how to install this database, see [Install Sample Data and Projects for the Analysis Services Multidimensional Modeling Tutorial](../analysis-services/install-sample-data-and-projects.md).  
  
In the task in this lesson, you define  KPIs in the [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] Tutorial project, and you then browse the [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] Tutorial cube by using these KPIs. You will define the following KPIs:  
  
-   Reseller Revenue  
  
    This KPI is used to measure how actual reseller sales compare to sales quotas for reseller sales, how close the sales are to the goal, and what the trend is toward reaching the goal.  
  
-   Product Gross Profit Margin  
  
    This KPI is used to determine how close the gross profit margin is for each product category to a specified goal for each product category, and also to determine the trend toward reaching this goal.  
  
## Defining the Reseller Revenue KPI  
  
1.  Open Cube Designer for the [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] Tutorial cube, and then click the **KPIs** tab.  
  
    The **KPIs** tab includes several panes. On the left side of the tab are the **KPI Organizer** pane and the **Calculation Tools** pane. The display pane in the middle of the tab contains the details of the KPI that is selected in the **KPI Organizer** pane.  
  
    The following image shows the **KPIs** tab of Cube Designer.  
  
    ![KPIs tab of Cube Designer](../analysis-services/media/l7-kpi-1.gif "KPIs tab of Cube Designer")  
  
2.  On the toolbar of the **KPIs** tab, click the **New KPI** button.  
  
    A blank KPI template appears in the display pane, as shown in the following image.  
  
    ![Blank KPI template in display pane](../analysis-services/media/l7-kpi-2.gif "Blank KPI template in display pane")  
  
3.  In the **Name** box, type **Reseller Revenue**, and then select **Reseller Sales** in the **Associated measure group** list.  
  
4.  On the **Metadata** tab in the **Calculation Tools** pane, expand **Measures**, expand **Reseller Sales**, and then drag the **Reseller Sales-Sales Amount** measure to the **Value Expression** box.  
  
5.  On the **Metadata** tab in the **Calculation Tools** pane, expand **Measures**, expand **Sales Quotas**, and then drag the **Sales Amount Quota** measure to the **Goal Expression** box.  
  
6.  Verify that **Gauge** is selected in the **Status indicator** list, and then type the following MDX expression in the **Status expression** box:  
  
    ```  
    Case  
     When   
      KpiValue("Reseller Revenue")/KpiGoal("Reseller Revenue")>=.95  
       Then 1  
     When  
      KpiValue("Reseller Revenue")/KpiGoal("Reseller Revenue")<.95  
       And   
      KpiValue("Reseller Revenue")/KpiGoal("Reseller Revenue")>=.85  
       Then 0  
      Else-1  
    End  
    ```  
  
    This MDX expression provides the basis for evaluating the progress toward the goal. In this MDX expression, if actual reseller sales are more than 85 percent of the goal, a value of 0 is used to populate the chosen graphic. Because a gauge is the chosen graphic, the pointer in the gauge will be half-way between empty and full. If actual reseller sales are more the 90 percent, the pointer on the gauge will be three-fourths of the way between empty and full.  
  
7.  Verify that **Standard arrow** is selected in the **Trend indicator** list, and then type the following expression in the **Trend expression** box:  
  
    ```  
    Case  
     When IsEmpty  
      (ParallelPeriod  
       ([Date].[Calendar Date].[Calendar Year],1,  
           [Date].[Calendar Date].CurrentMember))  
      Then 0    
     When  (  
      KpiValue("Reseller Revenue") -   
       (KpiValue("Reseller Revenue"),   
        ParallelPeriod  
         ([Date].[Calendar Date].[Calendar Year],1,  
           [Date].[Calendar Date].CurrentMember))  
          /  
          (KpiValue ("Reseller Revenue"),  
           ParallelPeriod  
            ([Date].[Calendar Date].[Calendar Year],1,  
             [Date].[Calendar Date].CurrentMember)))  
           >=.02  
      Then 1  
       When(  
        KpiValue("Reseller Revenue") -   
         (KpiValue ( "Reseller Revenue" ),  
          ParallelPeriod  
           ([Date].[Calendar Date].[Calendar Year],1,  
            [Date].[Calendar Date].CurrentMember))  
           /  
            (KpiValue("Reseller Revenue"),  
             ParallelPeriod  
              ([Date].[Calendar Date].[Calendar Year],1,  
                [Date].[Calendar Date].CurrentMember)))  
            <=.02  
      Then -1  
       Else 0  
    End  
    ```  
  
    This MDX expression provides the basis for evaluating the trend toward achieving the defined goal.  
  
## Browsing the Cube by Using the Reseller Revenue KPI  
  
1.  On the **Build** menu of [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], click **Deploy Analysis Service Tutorial**.  
  
2.  When deployment has successfully completed, on the toolbar of the **KPIs** tab, click the **Browser View** button, and then click **Reconnect**.  
  
    The status and trend gauges are displayed in the **KPI Browser** pane for reseller sales based on the values for the default member of each dimension, together with the value for the value and the goal. The default member of each dimension is the All member of the All level, because you have not defined any other member of any dimension as the default member.  
  
3.  In the filter pane, select **Sales Territory** in the **Dimension** list, select **Sales Territories** in the **Hierarchy** list, select **Equal** in the **Operator** list, select the **North America** check box in the **Filter Expression** list, and then click **OK**.  
  
4.  In the next row in the **Filter** pane, select **Date** in the **Dimension** list, select **Calendar Date** in the **Hierarchy** list, select **Equal** in the **Operator** list, select the **Q3 CY 2007** check box in the **Filter Expression** list, and then click **OK**.  
  
5.  Click anywhere in the **KPI Browser** pane to update the values for the **Reseller Revenue KPI**.  
  
    Notice that the **Value**, **Goal**, and **Status** sections of the KPI reflect the values for the new time period  
  
## Defining the Product Gross Profit Margin KPI  
  
1.  Click the **Form View** button on the toolbar of the **KPIs** tab, and then click the **New KPI** button.  
  
2.  In the **Name** box, type **Product Gross Profit Margin**, and then verify that **<All>** appears in the **Associated measure group** list.  
  
3.  In the **Metadata** tab in the **Calculation Tools** pane, drag the **Total GPM** measure to the **Value Expression** box.  
  
4.  In the **Goal Expression** box, type the following expression:  
  
    ```  
    Case  
        When [Product].[Category].CurrentMember Is  
          [Product].[Category].[Accessories]  
        Then .40                   
        When [Product].[Category].CurrentMember   
          Is [Product].[Category].[Bikes]  
        Then .12                  
        When [Product].[Category].CurrentMember Is  
          [Product].[Category].[Clothing]  
        Then .20  
        When [Product].[Category].CurrentMember Is  
          [Product].[Category].[Components]  
        Then .10  
        Else .12              
    End  
    ```  
  
5.  In the **Status indicator** list, select **Cylinder**.  
  
6.  Type the following MDX expression in the **Status expression** box:  
  
    ```  
    Case  
        When KpiValue( "Product Gross Profit Margin" ) /   
             KpiGoal ( "Product Gross Profit Margin" ) >= .90  
        Then 1  
        When KpiValue( "Product Gross Profit Margin" ) /   
             KpiGoal ( "Product Gross Profit Margin" ) <  .90  
             And   
             KpiValue( "Product Gross Profit Margin" ) /   
             KpiGoal ( "Product Gross Profit Margin" ) >= .80  
        Then 0  
        Else -1  
    End  
    ```  
  
    This MDX expression provides the basis for evaluating the progress toward the goal.  
  
7.  Verify that **Standard arrow** is selected in the **Trend indicator** list, and then type the following MDX expression in the **Trend expression** box:  
  
    ```  
    Case  
    When IsEmpty  
      (ParallelPeriod  
       ([Date].[Calendar Date].[Calendar Year],1,  
           [Date].[Calendar Date].CurrentMember))  
      Then 0    
       When VBA!Abs  
        (  
          KpiValue( "Product Gross Profit Margin" ) -   
           (  
             KpiValue ( "Product Gross Profit Margin" ),  
              ParallelPeriod  
              (   
                [Date].[ Calendar Date].[ Calendar Year],  
                1,  
                [Date].[ Calendar Date].CurrentMember  
              )  
            ) /  
            (  
              KpiValue ( "Product Gross Profit Margin" ),  
              ParallelPeriod  
              (   
                [Date].[ Calendar Date].[ Calendar Year],  
                1,  
                [Date].[ Calendar Date].CurrentMember  
              )  
            )    
          ) <=.02  
      Then 0  
      When KpiValue( "Product Gross Profit Margin" ) -   
           (  
             KpiValue ( "Product Gross Profit Margin" ),  
             ParallelPeriod  
             (   
               [Date].[ Calendar Date].[ Calendar Year],  
               1,  
               [Date].[ Calendar Date].CurrentMember  
             )  
           ) /  
           (  
             KpiValue ( "Product Gross Profit Margin" ),  
             ParallelPeriod  
             (   
               [Date].[Calendar Date].[Calendar Year],  
               1,  
               [Date].[Calendar Date].CurrentMember  
             )  
           )  >.02  
      Then 1  
      Else -1  
    End  
    ```  
  
    This MDX expression provides the basis for evaluating the trend toward achieving the defined goal.  
  
## Browsing the Cube by Using the Total Gross Profit Margin KPI  
  
1.  On the **Build** menu, click **Deploy Analysis Service Tutorial**.  
  
2.  When deployment has successfully completed, click **Reconnect** on the toolbar of the **KPIs** tab, and then click **Browser View**.  
  
    The **Product Gross Profit Margin** KPI appears and displays the KPI value for **Q3 CY 2007** and the **North America** sales territory.  
  
3.  In the **Filter** pane, select **Product** in the **Dimension** list, select **Category** in the **Hierarchy** list, select **Equal** in the **Operator** list, and then select **Bikes** in the **Filter Expression** list, and then click **OK**.  
  
    The gross profit margin for the sale of Bikes by resellers in North America in Q3 CY 2007 appears.  
  
## Next Lesson  
[Lesson 8: Defining Actions](../analysis-services/lesson-8-defining-actions.md)  
  
