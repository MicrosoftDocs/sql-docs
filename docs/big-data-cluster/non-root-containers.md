---
title: Non-root Big Data Clusters containers
titleSuffix: SQL Server Big Data Clusters
description: This article describes how to deploy non-root containers in SQL Server Big Data Clusters
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: hudequei
ms.date: 06/22/2020
ms.service: sql
ms.subservice: big-data-cluster
ms.topic: conceptual
---

# Non-root Big Data Clusters containers

[!INCLUDE[SQL Server 2019](../includes/applies-to-version/sqlserver2019.md)]

[!INCLUDE[big-data-clusters-banner-retirement](../includes/bdc-banner-retirement.md)]

SQL Server 2019 CU5 introduces support for non-root containers. The platform implementation is safer by ensuring that all container applications running within BDC are started as non-root users by default, on all supported platforms. These capabilities are available for all new deployments using the SQL Server 2019 CU5 corresponding image tag. Existing pre-CU5 BDC deployments will not be impacted by this change, and applications in these clusters will continue to run as root user. 

## Technical background

Please review [this technical whitepaper](https://aka.ms/sql-bdc-openshift-security) that captures details of the security design for accommodating deployments using non-root users, highlighting what and why Big Data Clusters temporarily elevate permissions is certain cases. The content of the whitepaper was developed in collaboration with security experts from SQL Server and Red Hat, and focuses on security contexts and capabilities in OpenShift, but the BDC security concepts and design are applicable to all supported platforms.

> [!NOTE]
> At the time of the CU5 release, the setup step of the applications deployed with [app deploy](concept-application-deployment.md) interfaces will still run as *root* user. This is required since during setup  additional packages that application will use are installed. Other user code deployed as part of the application will run as low privilege user. 

> [!NOTE]
> We recommend that the cluster runs with the default non-root setting. In case you want to revert back to pre-CU5 behavior, and have containers within BDC run as `root` user, you can use the new feature switch `allowRunAsRoot` and turn off the default behavior. You can only set this at deployment time. To set this, specify the setting under the `security` section in the `control.json` deployment configuration file:

```json
 "security": {
  …
    "allowRunAsRoot": true,
  …
}
```

> [!IMPORTANT]
> Changing this setting in OpenShift environments is not supported.

As a result of services within BDC running as non-root users, credentials used for connecting to services through gateway endpoint are not using `root` username. If the application used to connect to gateway endpoint is using the wrong credentials, you will see an authentication error. The new username for the gateway endpoint is based on the value passed through `AZDATA_USERNAME` environment variable. It is the same username used for the controller and SQL Server endpoints. This only impacts new deployments, existing big data clusters deployed with any of the previous releases continue to use `root`. There is no impact to credentials when the cluster is deployed to use Active Directory authentication. 

## Use the latest Azure Data Studio

Azure Data Studio handles the credentials change transparently for the connection made through gateway to enable HDFS browsing experience in the Object Explorer or submitting Spark jobs through notebooks. Install the [latest Azure Data Studio insiders build](../azure-data-studio/download-azure-data-studio.md#download-the-insiders-build-of-azure-data-studio). This build includes the necessary changes for this use case.

For other scenarios where you must provide credentials for accessing the service through the gateway (e.g. logging in with [!INCLUDE [azure-data-cli-azdata](../includes/azure-data-cli-azdata.md)], accessing web dashboards for Spark), ensure the correct credentials are used. If you are targeting an existing cluster deployed before CU5 you will continue using `root` username to connect to gateway, even after upgrading the cluster to CU5. If you deploy a new cluster using CU5 build, you will login by providing the username corresponding to `AZDATA_USERNAME` environment variable.

## Configuration file switches

Beginning with CU5, two new feature switches were added to control the collection of pod and node metrics. In case you are using different solutions for monitoring your Kubernetes infrastructure, you can turn of the built-in metrics collection for pods and host nodes by setting `allowNodeMetricsCollection` and `allowPodMetricsCollection`* to `false` in `control.json` deployment configuration file. 

For example 

```json
"security": {
  ...
  "allowPodMetricsCollection": true,
  "allowNodeMetricsCollection": true,
  ...
}
```

> [!NOTE]
> For OpenShift environments, these settings are set to false by default in the built-in deployment profiles. Collecting pod and node metrics required privileged capabilities and the recommended security context for OpenShift is based off the *restricted* constraints.

In addition to non-privileged containers, starting with CU5, for all new deployments for BDC, containers run as a non-root user by default on all supported platforms. These capabilities are available for all new deployments using the SQL Server 2019 CU5 corresponding image tag. Existing pre-CU5 BDC deployments will not be impacted, and applications in these clusters will continue to run as root user.

## Next steps
[How to deploy [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)] on Kubernetes](deployment-guidance.md)

[Deploy [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)] on OpenShift](deploy-openshift.md)

[Security whitepaper](https://aka.ms/sql-bdc-openshift-security)
