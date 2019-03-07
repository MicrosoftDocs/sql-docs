---
title: "Customize the Parameters Pane in a Report (Report Builder) | Microsoft Docs"
ms.date: 03/01/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-native"
ms.technology: report-design

ms.topic: conceptual
ms.assetid: 4ce9e8d5-911a-4422-928f-a8d005b79fc6
author: markingmyname
ms.author: maghan
---
# Customize the Parameters Pane in a Report (Report Builder)
  When creating paginated reports with parameters in Report Builder, you can customize the Parameters pane. In report design view, you can drag a parameter to a specific column and row in the Parameters pane. You can add and remove columns to change the layout of the pane.  
  
 When you drag a parameter to a new column and row in the pane, the parameter order changes in the **Report Data** pane. When you change the order of the parameter in the **Report Data** pane, the location of the parameter in the pane is changed. For more information about why parameter order is important, see [Change the Order of a Report Parameter &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/change-the-order-of-a-report-parameter-report-builder-and-ssrs.md).  
  
## To customize the parameters pane  
  
1.  On the **View** tab, select the **Parameters** checkbox to display the parameters pane.  
  
     ![Access parameters pane from View tab](../../reporting-services/report-design/media/ssrs-customparameter-accessparameterpanedesignmode.png "Access parameters pane from View tab")  
  
     The pane appears at the top of the design surface.  
  
2.  To add a parameter to the pane, do one of the following.  
  
    -   Right click an empty cell in the parameters pane, and then click **Add Parameter**.  
  
         ![Add new parameter from parameters pane](../../reporting-services/report-design/media/ssrs-customizeparameter-addnewparameter.png "Add new parameter from parameters pane")  
  
    -   Right click **Parameters** in the **Report Data** pane, and then click **Add Parameter**.  
  
3.  To move a parameter to a new location in the parameters pane, drag the parameter to a different cell in the pane.  
  
     When you change the location of the parameter in the pane, the order of the parameter in the **Parameters** list in the **Report Data** pane is automatically changed. For more information about the impact of the parameter order, see [Change the Order of a Report Parameter &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/change-the-order-of-a-report-parameter-report-builder-and-ssrs.md)  
  
4.  To access the properties for a parameter, do one of the following.  
  
    -   Right click the parameter in the parameters pane, and then click **Parameter Properties**.  
  
         ![Access parameter properties from the parameters pane](../../reporting-services/report-design/media/ssrs-customizeparameter-accessparameterproperties-composite.png "Access parameter properties from the parameters pane")  
  
    -   Right click the parameter in the **Report Data** pane, and then click **Parameter Properties**.  
  
5.  To add new columns and rows to the pane, or delete existing rows and columns, right click anywhere in the parameters pane and then click a command on the menu that displays.  
  
     ![Add columns and rows to the parameters pane](../../reporting-services/report-design/media/ssrs-customparameter-addcolumnsrows.png "Add columns and rows to the parameters pane")  
  
    > [!IMPORTANT]  
    >  When you delete a column or row that contains parameters, the parameters are deleted from the report.  
  
6.  To delete a parameter from the pane and from the report, do one of the following.  
  
    -   Right click the parameter in the parameters pane, and then click  **Delete**.  
  
         ![Delete parameters from the parameters pane](../../reporting-services/report-design/media/ssrs-customparameter-deleteparameter.png "Delete parameters from the parameters pane")  
  
    -   Right click parameter in the **Report Data** pane, and then click **Delete**.  
  
## See Also  
 [Report Parameters &#40;Report Builder and Report Designer&#41;](../../reporting-services/report-design/report-parameters-report-builder-and-report-designer.md)  
  
  
