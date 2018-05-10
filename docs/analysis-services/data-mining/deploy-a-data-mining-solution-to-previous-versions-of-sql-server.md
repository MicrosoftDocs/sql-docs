---
title: "Deploy a Data Mining Solution to Previous Versions of SQL Server | Microsoft Docs"
ms.date: 05/01/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: data-mining
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Deploy a Data Mining Solution to Previous Versions of SQL Server
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  This section describes known compatibility issues that may arise when you attempt to deploy a data mining model or data mining structure that was created in an instance of [!INCLUDE[ssASCurrent](../../includes/ssascurrent-md.md)] to a database that uses SQL Server 2005 Analysis Services, or when you deploy models created in SQL Server 2005 to an instance of [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
 Deployment to an instance of SQL Server 2000 Analysis Services is not supported.  
  
 [Deploying Time Series Models](#bkmk_TimeSeries)  
  
 [Deploying Models with Holdout](#bkmk_Holdout)  
  
 [Deploying Models with Filters](#bkmk_Filter)  
  
 [Restoring from Database Backups](#bkmk_Backup)  
  
 [Using Database Synchronization](#bkmk_Synch)  
  
##  <a name="bkmk_TimeSeries"></a> Deploying Times Series Models  
 The Microsoft Time Series algorithm was enhanced in SQL Server 2008 by the addition of a second, complementary algorithm, ARIMA. For more information about the changes in the time series algorithm, see [Microsoft Time Series Algorithm](../../analysis-services/data-mining/microsoft-time-series-algorithm.md).  
  
 Therefore, time series mining models that use the new ARIMA algorithm may behave differently when deployed to an instance of SQL Server 2005 Analysis Services.  
  
 If you have explicitly set the parameter PREDICTION_SMOOTHING to control the mixture of ARTXP and ARIMA models in prediction, when you deploy this model to an instance of SQL Server 2005, Analysis Services will raise an error stating that the parameter is not valid. To prevent this error, you must delete the PREDICTION_SMOOTHING parameter and convert the models to a pure ARTXP model.  
  
 Conversely, if you deploy a time series model that was created using SQL Server 2005 Analysis Services to an instance of [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], when you open the mining model in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], the definition files are first converted to the new format, and two new parameters are added by default to all time series models. The parameter FORECAST_METHOD is added with the default value of MIXED, and the parameter PREDICTION_SMOOTHING is added with the default value of 0.5. However, the model will continue to use only ARTXP for forecasting until you reprocess the model. As soon as you reprocess the model, prediction changes to use both ARIMA and ARTXP.  
  
 Therefore, if you wish to avoid changing the model, you should only browse the model and never process it. Alternatively, you could explicitly set the FORECAST_METHOD or PREDICTION_SMOOTHING parameters.  
  
 For detailed information about configuring mixed models, see [Microsoft Time Series Algorithm Technical Reference](../../analysis-services/data-mining/microsoft-time-series-algorithm-technical-reference.md).  
  
 If the provider that is used for the model's data source is SQL Client Data Provider 10, you must also modify the data source definition to specify the previous version of the SQL Server Native Client. Otherwise, [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)] generates an error stating that the provider is not registered.  
  
##  <a name="bkmk_Holdout"></a> Deploying Models with Holdout  
 If you create a mining structure that contains a holdout partition used for testing data mining models, the mining structure can be deployed to an instance of SQL Server 2005, but the partition information will be lost.  
  
 When you open the mining structure in SQL Server 2005 Analysis Services, [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)] raises an error, and then regenerates the structure to remove the holdout partition.  
  
 After the structure has been rebuilt, the size of the holdout partition is no longer available in the Properties window; however, the value \<ddl100_100:HoldoutMaxPercent>30\</ddl100_100:HoldoutMaxPercent>) may still be present in the ASSL script file.  
  
##  <a name="bkmk_Filter"></a> Deploying Models with Filters  
 If you apply a filter to a mining model, the model can be deployed to an instance of SQL Server 2005, but the filter will not be applied.  
  
 When you open the mining model, [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] raises an error, and then regenerates the model to remove the filter.  
  
##  <a name="bkmk_Backup"></a> Restoring from Database Backups  
 You cannot restore a database backup that was created in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] to an instance of SQL Server 2005. If you do so, SQL Server Management Studio generates an error.  
  
 If you create a backup of a SQL Server 2005 Analysis Services database and restore this backup on an instance of [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], all time series models are modified as described in the previous section.  
  
##  <a name="bkmk_Synch"></a> Using Database Synchronization  
 Database synchronization is not supported from [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] to SQL Server 2005.  
  
 If you attempt to synchronize a [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] database, the server returns an error and database synchronization fails.  
  
## See Also  
 [Analysis Services Backward Compatibility](../../analysis-services/analysis-services-backward-compatibility.md)  
  
  
