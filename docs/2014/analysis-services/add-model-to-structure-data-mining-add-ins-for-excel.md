---
title: "Add Model to Structure (Data Mining Add-ins for Excel) | Microsoft Docs"
ms.custom: ""
ms.date: "12/29/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "mining models, creating"
ms.assetid: 8efd5bf4-4e6a-4ee8-971a-6efaed5f3b76
author: minewiskan
ms.author: owend
manager: craigg
---
# Add Model to Structure (Data Mining Add-ins for Excel)
  ![Add Model to Structure button](media/dmc-addmodel.gif "Add Model to Structure button")  
  
 When you click **Add Model to Structure**, a wizard starts that helps you create a new mining model to use with an existing mining structure. This option is useful because it lets you compare models that are based on the same data, or create customized models.  
  
 If the instance of Analysis Services does not already contain the data you need, use the [Create Mining Structure &#40;SQL Server Data Mining Add-ins&#41;](create-mining-structure-sql-server-data-mining-add-ins.md) wizard to set up a mining structure. Or, you can launch one of the modeling wizards, and then add a new model to the structure created by the wizard.  
  
 To create advanced models using algorithms not supported by the wizards, create a mining structure and then add a model using the **Data Mining Advanced Query Editor**.  
  
## Add a new model to an existing structure  
  
1.  On the **Data Mining** ribbon, click the arrow under **Advanced**, and then select **Add Model to Structure**.  
  
2.  In the **Select Structure** dialog box, choose the structure that contains the data you want to use, and then click **Next**.  
  
     **Tip**: If you are not sure which mining structure contains the data you need, use the **Document Model** wizard to view the columns and basic statistics about the data.  
  
     If you can't find a mining structure, check the connection that you are currently using. You might need to open a connection to a different server.  
  
3.  In the **Select Mining Algorithm** dialog box, choose a mining algorithm to use in the new mining model.  
  
     Notice that the dialog box provides a lot more options than you'll see in the wizards. You can create a model using any algorithm supported on your [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] server, provided your data is compatible.  
  
4.  We recommend that you also click the **Parameters** button to open the **Algorithm Parameters** dialog box and customize parameters on the algorithm. This option is the easiest way to create custom mining models.  
  
5.  Click **Next**.  
  
6.  In the **Select Columns** dialog box, review the list of columns, and if necessary, change the usage of the columns to one of these values:  
  
    -   **Input**. Indicates that the column contains variables that may affect the outcome and should be used as inputs to the model.  
  
    -   **Input and Predict**. Indicates that the data should be used as an input, and that you also want to predict these values.  
  
    -   **Predict only**. Indicates that the data should not be used as an input for the model.  
  
    -   **Key**. Each model requires at least one key. Depending on the model type, you might also have the option to additional special keys, such as the **SequenceKey** or **TimeKey**.  
  
    -   **Do not use**. Indicates that the data should not be used in the model, even if present in the structure.  
  
7.  Click the browse **(...)** button to open the **Set Column Model Flags** dialog box.  
  
     Take a minute to verify that the usage of each data column is appropriate for the model. This is an important step for preventing errors when you try to process the model.  
  
     For example, if you re-use a structure that was created for a decision trees model and apply the Naïve Bayes algorithm to it, columns that have the data type `Numeric` and the content type `Continuous` will need to be binned or changed to discrete variables.  
  
     If columns in the structure are not applicable to the new algorithm, you can bypass them by selecting **Do not use**.  
  
8.  In the **Set Column Model Flags** dialog box, review or set the modeling flags, if any.  
  
     Modeling flags let you control the way that nulls are handled, among other things. For more information, see [Modeling Flags &#40;Data Mining&#41;](data-mining/modeling-flags-data-mining.md).  
  
     Click **OK** when done to close the dialog box.  
  
9. In the **Finish** dialog box, type a name and description for the new mining model.  
  
     Depending on the type of model you built, you might also have these options:  
  
    -   Browse the completed mining model after it is built.  
  
    -   Use drillthrough from the model to the source data.  
  
         For more information, see [Drillthrough on Mining Models](data-mining/drillthrough-on-mining-models.md).  
  
10. Click **Finish** to save your changes. As you do so the new model is deployed to the server and processed.  
  
### Related Options  
  
|Option|Comments|  
|------------|--------------|  
|**Select Structure or Model** dialog box|Choose en existing mining structure to use as the basis for building a new model.  The structure you pick must be located on the current connection. If not, change connections using the [Connect to Source Data &#40;Data Mining Client for Excel&#41;](connect-to-source-data-data-mining-client-for-excel.md) tool.|  
|**Select Mining Algorithm** dialog Box|The list of data mining algorithms depends on which server you are connected to. [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] provides different algorithms in the Standard and Enterprise editions. Your administrator also might have added custom algorithms.<br /><br /> If you can't see any algorithms, verify that you are connected to an instance of [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)].|  
|**Algorithm Parameters** Dialog Box|In these settings, you can customize each algorithm using parameters specific to the analytical method. You can also set a seed to ensure that the results of the model can be reproduced across multiple training passes.<br /><br /> For more information, see [Algorithm Parameters &#40;SQL Server Data Mining Add-ins&#41;](algorithm-parameters-sql-server-data-mining-add-ins.md).|  
|**Set Column Model Flags** Dialog Box|Modeling flags can improve your model by specifying how missing data is to be handled. For more information, see [Modeling Flags &#40;Data Mining&#41;](data-mining/modeling-flags-data-mining.md).|  
  
###  <a name="Bkmk_mdlcolumn"></a> Setting Column Usage  
 When you add a new model to an existing mining structure, you must specify how the model will use each of the columns of data in the mining structure. You'll probably observe that the options in this wizard are far more detailed than the options on the mining structure. Why?  
  
 The reason is that when you create a model and a structure together using a wizard, many of the options that control how data is used by the algorithm are set automatically. However, when you add a new model to an existing, you need to see these options manually, and specify whether data should be used for analysis, whether the data type is correct, and so forth.  
  
 You might get error messages when applying new algorithms to existing data, but these messages generally provide detailed information about the corrections you need to make to allow the model to be processed. Typical problems include the following:  
  
-   The model expects a different data type than the structure contains.  
  
     Some algorithms can only work with numbers; some can only work with text. If your data is the wrong type for the new model, you might need to modify the structure to enable the model to process.  
  
-   The mining structure contains no predictable attribute.  
  
     Clustering models can be built with no predictable value, but other models generally require that you specify a single column for prediction.  
  
-   The composition of the data is incompatible with the algorithm you've chosen.  
  
     Some types of analysis require data that is carefully structured according to unique rules. Examples are forecasting models and association models. You can easily add new models of the same type, perhaps with customizations, but the data might not work with other algorithms.  
  
### Requirements  
 To create data mining models, you must have a connection to an instance of [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)]. For more information about how to create or change a connection, see [Connect to Source Data &#40;Data Mining Client for Excel&#41;](connect-to-source-data-data-mining-client-for-excel.md).  
  
 If you cannot see the data mining structure that you want, it could be that the structure was saved to a different instance or different [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] database. For information about how to change to a different data mining connection, see [Connect to a Data Mining Server](connect-to-a-data-mining-server.md).  
  
## See Also  
 [Creating a Data Mining Model](creating-a-data-mining-model.md)   
