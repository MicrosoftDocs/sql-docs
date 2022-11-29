---
title: "Specify the size of an indicator in a paginated report using an expression | Microsoft Docs"
description: Discover ways to use size, in addition to color, direction, and shape, to maximize the visual impact of indicators in a paginated report in Report Builder.
ms.date: 03/01/2017
ms.service: reporting-services
ms.subservice: report-design


ms.topic: conceptual
ms.assetid: ab0b86f1-4882-4258-a2b6-c612faecfa4b
author: maggiesMSFT
ms.author: maggies
---
# Specify the size of an indicator in a paginated report using an expression (Report Builder)

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-ssrs-rb](../../includes/ssrs-appliesto-ssrs-rb.md)] [!INCLUDE [ssrs-appliesto-pbi-rb](../../includes/ssrs-appliesto-pbi-rb.md)] [!INCLUDE [ssrb-applies-to-ssdt-yes](../../includes/ssrb-applies-to-ssdt-yes.md)]

  In addition to color, direction, and shape, you can use size to maximize the visual impact of indicators in a paginated report.  
  
 An indicator has a collection of indicator states named IndicatorStates. The IndicatorStates collection typically have multiple states. Each state is a member of the collection and is represented by an icon. Together the states constitute the IndicatorsStates collection.  
  
 To dynamically configure the sizes of icons, you set properties of members of the IndicatorsStates collection in the Properties pane of Report Builder. If the **Properties** pane is not visible, click the **View** tab and select **Properties**.  
  
> [!NOTE]  
>  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], you use the **Properties** window to set the member properties. If the **Properties** window is not open, press the F4 key.  
  
 The **Properties** pane provides access to the properties of the IndicatorStates collection of an indicator. You configure the icons to be different sizes by setting the ScaleFactor property of the IndicatorStates collection members using an expression. For more information, see [Expressions &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/expressions-report-builder-and-ssrs.md).  
  
 The expression used in this procedure was also used to generate the report with different sizes of indicators, shown in [Indicators &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/indicators-report-builder-and-ssrs.md).  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
### To specify the indicator icon size using an expression  
  
1.  Click the indicator you want to change.  
  
2.  In the Properties pane, locate the IndicatorStates property.  
  
     If the Properties pane is organized by category, you will find IndicatorStates in the **States** category.  
  
3.  Click the ellipsis **(...)** button next to IndicatorStates. The **IndicatorState Collection Editor** dialog box opens.  
  
     Select all members of the collection.  
  
4.  In the **Multi-Select Properties** list, click the down arrow next to ScaleFactor and then click **Expression**.  
  
5.  In the **Expression** dialog box write the expression.  
  
     The following sample expression makes the icon a different size based on the value of the **SalesYTD** field.  
  
     `=IIF(Fields!SalesYTD.value = 0,0,Fields!SalesYTD.value/Max(Fields!SalesYTD.value,"Indicator"))`  
  
     For more information, see [Expression Examples &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/expression-examples-report-builder-and-ssrs.md).  
  
6.  Select **OK**.
  
7.  Select **OK**.
  
## See Also  
 [Indicators &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/indicators-report-builder-and-ssrs.md)  
  
  
