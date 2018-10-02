---
title: Monitoring Cluster using Cluster Admin Portal | Microsoft Docs
description:
author: yualan
ms.author: alayu
manager: craigg
ms.date: 10/01/2018
ms.topic: conceptual
ms.prod: sql
---
# Introduction to the Cluster Admin Portal
If you want to monitor or troubleshoot your SQL Server big data cluster, use the Cluster Admin Portal. 

The Cluster Admin Portal allows you to:
- Quickly view number of pods running and any issues
- Monitor deployment status
- View available service endpoints
- View Controller and Master Instance
- Drill down information on pods, including accessing Grafana dashboards and Kibana logs

## Access Cluster Admin Portal
Follow the [quickstart to deploy your big data cluster](quickstart-big-data-cluster-deploy.md) until you get to the **Cluster Administration Portal** section. Once you have the big data cluster running with mssqlctl, follow these instructions:

Once the Controller pod is running, you can use the Cluster Administration Portal to monitor the deployment. You can access the portal using the external IP address and port number for the `service-proxy-lb` (for example: **https://\<ip-address\>:30777**). Credentials for accessing the admin portal are the values of `CONTROLLER_USERNAME` and `CONTROLLER_PASSWORD` environment variables provided above.
> [!NOTE]
> There is going to be a security warning when accessing the web page since we are using auto-generated SSL certificates. In future releases, we will provide the capability to provide your own signed certificates.

## Overview
![overview](./media/cluster-admin-portal/portal-overview.png)

When you first enter the portal, you can quickly view the number of pods running in:
- Controller
- Master Instance
- Compute Pool
- Storage Pool
- Data Pool

If there are any issues, you can open a link to known issues. You can use the left nav pane to go to the specific pool if there is an issue.

## Deployment
![deployment](./media/cluster-admin-portal/portal-deployment.png)

To monitor your deployment, click on the deployment tab on the left. You can see a tree view of your deployment, and if there are any issues in the deployment.

## Service Endpoints
![endpoints](./media/cluster-admin-portal/portal-endpoints.png)

You can view available service endpoints by clicking on the endpoints tab on the left nav pane.

This includes links to Spark endpoint, Grafana dashboard, and Kibana logs.

## Controller
![controller](./media/cluster-admin-portal/portal-controller.png)

The controller shows all pods related to the controller. You can learn more about the controller [here.](concept-controller.md)

To learn more additional information on each pod, you can click on **Metrics** column to view Grafana dashboards.

To learn about logs for pods with issues, you can click on the **Logs** column.

## Master Instance
![endpoints](./media/cluster-admin-portal/portal-master.png)

The Master Instance shows all pods related to the SQL Server in master. You can learn more about the Master Instance [here.](concept-master-instance.md)

To learn more additional information on each pod, you can click on **Metrics** column to view Grafana dashboards.

To learn about logs for pods with issues, you can click on the **Logs** column.

## Pool and Pod Pages
![pool](./media/cluster-admin-portal/portal-data-pool.png)

On every pool page (Compute, Storage, and Data), you can drill down to each of the pod pages by clicking on **Default**

![pod](./media/cluster-admin-portal/portal-data-default-pool.png)

This shows a breadcrumb on the top of the drill down path.

To learn more additional information on each pod, you can click on **Metrics** column to view Grafana dashboards.

To learn about logs for pods with issues, you can click on the **Logs** column.

To learn more about each pool:
- [compute pool](concept-compute-pool.md)
- [storage pool](concept-storage-pool.md)
- [data pool](concept-data-pool.md)

## About Page
![about](./media/cluster-admin-portal/portal-about.png)

Here you can view information about your cluster such as the different version numbers, containers, and a link to our documentation.
