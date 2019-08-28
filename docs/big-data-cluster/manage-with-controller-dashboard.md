---
title: Manage SQL Server big data cluster with controller dashboard
titleSuffix: Manage SQL Server big data cluster with controller dashboard
description: Use a notebook from Azure Data Studio to manage and troubleshoot a big data cluster.
author: yualan
ms.author: alanyu
ms.reviewer: mikeray
ms.date: 08/29/2019
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
---

# Manage big data clusters for SQL Server controller dashboard

[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

In addition to azdata and the cluster status notebook, there is another way to view the status of your SQL Server Big Data Cluster. You can now add the SQL Server big data cluster controller through the **Connections** viewlet. This will then enable you to have a dashboard where you can view the cluster health.

![dashboard](media/manage-with-controller-dashboard/controller-dashboard.png)
## Prerequisites

The following prerequisites are required to be able to launch the notebook:

* Latest version of [Azure Data Studio Insiders build](https://docs.microsoft.com/sql/big-data-cluster/deploy-big-data-tools?view=sqlallproducts-download-and-install-azure-data-studio-sql-server-2019-release-candidate-rc)
* [!INCLUDE[sql-server-2019](../includes/sssqlv15-md.md)] extension installed in Azure Data Studio

In addition to above, deploying SQL Server 2019 big data cluster also requires:

* [azdata](deploy-install-azdata.md)
* [kubectl](https://kubernetes.io/docs/tasks/tools/install-kubectl/#install-kubectl-binary-using-native-package-management)
* [Azure CLI](/cli/azure/install-azure-cli)


## Add SQL Server big data cluster controller
1. Click on the Connections view on the left pane.
2. At the bottom of the Connections view, **SQL Server Big Data Clusters** to expand it.
3. Click **Add SQL Server big data cluster controller**, this will launch a new dialog.

## Add New Controller
1. Add your cluster name.
2. Add your Cluster Management Service URL. This can be found on the Service Endpoints table in your BDC dashboard, or ask your cluster admin.
3. Add your username and password.

## Launch controller dashboard
1. When you successfully add your controller, you can view it under SQL Server Big Data Clusters.
2. To launch the dashboard, right click on the controller and click **Manage.**

## Controller Dashboard
1. On the Controller dashboard, there are 3 main components:
    a. **Toolbar** on top which contain actions for the dashboard.
    b. **Nav pane** on left which changes to the different views on the dashboard.
    c. **View** covering the majority of the page.
2. On the **Big data cluster overview** page, you can see:
    a. **Cluster Properties** to see information about your cluster.
    b. **Cluster Overview** to see high level overview of all the cluster components, and which ones are unhealthy
    c. **Service Endpoints** to copy or access to different services within your SQL Server Big Data Cluster.
3. On the **Nav pane,** you can see the list of services and click on one to view additional cluster details. This is the same views when you click on a service in **Cluster Overview.**
4. Each view under **Cluster Details** consist of the same UI components:
    a. **Health Status Details** which sshare the state and health status of the component.
    b. **Metrics and Logs** to view additional metrics and logs through Grafana and Kibana.
5. If you view a component that is unhealthy, you can click on **Troubleshoot** on the toolbar to launch a Jupyter Book containing a notebook which can help diagnose your issue.

## Next Steps
For more information about the controller, see [our controller documentation](concept-controller.md).