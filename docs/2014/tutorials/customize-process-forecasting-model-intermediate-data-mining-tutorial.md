---
title: "Customizing and Processing the Forecasting Model (Intermediate Data Mining Tutorial) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: analysis-services
ms.topic: conceptual
ms.assetid: 4bd25e15-9d9e-4528-b7bc-ccb856643aec
author: minewiskan
ms.author: owend
manager: kfile
---
# Customizing and Processing the Forecasting Model (Intermediate Data Mining Tutorial)
  The [!INCLUDE[msCoName](../includes/msconame-md.md)] Time Series algorithm provides parameters that affect how a model is created, and how time data is analyzed. Changing these properties can significantly affect how the mining model makes predictions.  
  
 For this task in the tutorial, you will perform the following tasks to modify the model:  
  
1.  You will customize the way your model handles time periods by adding a new value for the *PERIODICITY_HINT* parameter.  
  
2.  You will learn about two other important parameters for the Microsoft Time Series algorithm: FORECAST_METHOD, which lets you control the method used for forecasting, and PREDICTION_SMOOTHING, which lets you customize the blend of long-term and short-term predictions.  
  
3.  Optionally, you will tell the algorithm how you want missing values to be imputed.  
  
4.  After all the changes have been made, you will deploy and process the model.  
  
## Setting Time Series Parameters  
 **Periodicity Hints**  
  
 The *PERIODICITY_HINT* parameter provides the algorithm with information about additional time periods that you expect to see in the data. By default, time series models will try to automatically detect a pattern in the data. However, if you already know the expected time cycle, providing a periodicity hint can potentially improve the accuracy of the model. However, if you provide the wrong periodicity hint, it can decrease accuracy; therefore, if you are not sure what value should be used, it is best to use the default.  
  
 For example, the view used for this model aggregates sales data from [!INCLUDE[ssAWDWsp](../includes/ssawdwsp-md.md)] on a monthly basis. Therefore each time slice used by the model represents one month, and all predictions will also be in terms of months. Since there are 12 months in a year and you expect that sales patterns more or less repeat on a yearly basis, you will set the *PERIODICITY_HINT* parameter to `12`, to indicate that 12 time slices (months) constitute one complete sales cycle.  
  
 **Forecasting Method**  
  
 The *FORECAST_METHOD* parameter controls whether the time series algorithm is optimized for short-term or long-term predictions. By default, the *FORECAST_METHOD* parameter is set to MIXED, which means that two different algorithms are blended and balanced to provide good results for both short-term and long-term prediction.  
  
 However, if you know that you want to use a particular algorithm, you can change the value to either ARIMA or ARTXP.  
  
 **Weighting Long-Term vs. Short-Term Predictions**  
  
 You can also customize the way that long-term and short-term predictions are combined by using the PREDICTION_SMOOTHING parameter. By default, this parameter is set to 0.5, which generally provides the best balance for overall accuracy.  
  
#### To change the algorithm parameters  
  
1.  On the **Mining Models** tab, right-click **Forecasting**, and select **Set Algorithm Parameters**.  
  
2.  In the `PERIODICITY_HINT` row of the **Algorithm Parameters** dialog box, click the **Value** column, then type `{12}`, including the braces.  
  
     By default, the algorithm will also add the value {1}.  
  
3.  In the `FORECAST_METHOD` row, verify that the **Value** text box is either blank or set to `MIXED`. If a different value has been entered, type `MIXED` to change the parameter back to the default value.  
  
4.  In the **PREDICTION_SMOOTHING** row, verify that the **Value** text box is either blank or set to 0.5. If a different value has been entered, click **Value** and type `0.5` to change the parameter back to the default value.  
  
    > [!NOTE]  
    >  The PREDICTION_SMOOTHING parameter is available only in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Enterprise. Therefore, you cannot view or change the value of the PREDICTION_SMOOTHING parameter in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Standard. However, the default behavior is to use both algorithms and weight them equally.  
  
5.  Click **OK**.  
  
## Handling Missing Data (Optional)  
 In many cases, your sales data might have gaps that are filled with nulls, or a store might have failed to meet the reporting deadline, leaving an empty cell at the end of the series. In such scenarios, [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] raises the following error and will not process the model.  
  
 "Error (Data mining): Time stamps not synchronized starting with series \<series name>, of the mining model, \<model name>. All time series must end at the same time mark and cannot have arbitrarily missing data points. Setting the MISSING_VALUE_SUBSTITUTION parameter to Previous or to a numeric constant will automatically patch missing data points where possible."  
  
 To avoid this error, you can specify that [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] automatically provide new values to fill in the gaps by using any one of the following methods:  
  
-   Using an average value. The mean is calculated by using all valid values in the same data series.  
  
-   Using the previous value. You can substitute previous values for multiple missing cells, but you cannot fill starting values.  
  
-   Using a constant value that you supply.  
  
#### To specify that gaps be filled by averaging values  
  
1.  On the **Mining Models** tab, right-click the **Forecasting** column, and select **Set Algorithm Parameters**.  
  
2.  In the **Algorithm Parameters** dialog box, in the **MISSING_VALUE_SUBSTITUTION** row, click the **Value** column, and type `Mean`.  
  
## Build the Model  
 To use the model, you must deploy it to a server, and process the model by running the training data through the algorithm.  
  
#### To process the forecasting model  
  
1.  On the **Mining Model** menu of [!INCLUDE[ssBIDevStudio](../includes/ssbidevstudio-md.md)], select **Process Mining Structure and All Models**.  
  
2.  At the warning asking whether you want to build and deploy the project, click **Yes**.  
  
3.  In the **Process Mining Structure - Forecasting** dialog box, click **Run**.  
  
     The **Process Progress** dialog box opens to display information about model processing. Model processing may take some time.  
  
4.  After processing is complete, click **Close** to exit the **Process Progress** dialog box.  
  
5.  Click **Close** again to exit the **Process Mining Structure - Forecasting** dialog box.  
  
## Next Task in Lesson  
 [Exploring the Forecasting Model &#40;Intermediate Data Mining Tutorial&#41;](../../2014/tutorials/exploring-the-forecasting-model-intermediate-data-mining-tutorial.md)  
  
## See Also  
 [Microsoft Time Series Algorithm Technical Reference](../../2014/analysis-services/data-mining/microsoft-time-series-algorithm-technical-reference.md)   
 [Microsoft Time Series Algorithm](../../2014/analysis-services/data-mining/microsoft-time-series-algorithm.md)   
 [Processing Requirements and Considerations &#40;Data Mining&#41;](../../2014/analysis-services/data-mining/processing-requirements-and-considerations-data-mining.md)  
  
  
