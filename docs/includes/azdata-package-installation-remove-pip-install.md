---
author: MikeRayMSFT
ms.author: mikeray
ms.date: 01/07/2020
ms.service: sql
ms.topic: include
---

### Python/Pip installation

You can install [!INCLUDE [azure-data-cli-azdata](../includes/azure-data-cli-azdata.md)]on Linux with yum, apt, or zypper, or on MacOS with Homebrew installation package managers. Before these package managers were available, installation required Python and pip.

>[!IMPORTANT]
>Before you proceed, you need to remove any installation of `azdata` that were installed to the global system Python. The new installers or native-packages add `azdata` to your path and it is impossible to know which one is first.
If you have an existing `azdata` installed to the global system Python, remove it before proceeding.

To view your current installation, run the following command:

```bash
$ pip list --format columns
```

If `azdata` is installed by pip it returns the package and version. For example:

```
 Package             Version
------------------- ----------
azdata-cli          15.0.X
azdata-cli-app      15.0.X
azdata-cli-cluster  15.0.X
azdata-cli-core     15.0.X
azdata-cli-hdfs     15.0.X
azdata-cli-notebook 15.0.X
azdata-cli-profile  15.0.X
azdata-cli-spark    15.0.X
azdata-cli-sql      15.0.X
```

The following example removes a pip installation of `azdata`.

```bash
$ pip freeze | grep azdata-* | xargs pip uninstall -y
```

After you have verified that you have removed any installation of `azdata` that was installed with pip, proceed with your installation.