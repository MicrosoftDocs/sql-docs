## Python/Pip installation

You can install `azdata` on Linux or MacOS with yum, upt, zypper, or Homebrew installation package managers. Before these package managers were available, installation required Python and pip.

>[!IMPORTANT]
>Before you proceed, you need to remove any installation of `azdata` that were installed to the global system Python. The new installers or native-packages will `azdata` to your path and it is impossible to know which one is first.
If you have an existing `azdata` installed to the global system Python, remove it before proceeding.

```bash
$ pip list --format columns
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

$ pip freeze | grep azdata-* | xargs pip uninstall -y


After you have verified that you have removed any installation of `azdata` that was installed with pip, proceed with your installation.