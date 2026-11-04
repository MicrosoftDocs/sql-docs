---
title: Install the PHP Drivers on Linux and macOS
description: Install the Microsoft Drivers for PHP for SQL Server and their ODBC dependency on Linux and macOS.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, sumitsar, jathakkar
ms.date: 11/02/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
ms.custom: intro-installation, linux-related-content
---

# Install the Microsoft Drivers for PHP on Linux and macOS

<a id="linux-and-macos-installation-tutorial-for-the-microsoft-drivers-for-php-for-sql-server"></a>

These instructions install PHP, the Microsoft ODBC Driver for SQL Server, and the SQLSRV and PDO_SQLSRV extensions. Each tab uses the PHP package provided by the repository that tab installs from, so the version in the commands differs from one tab to the next. Replace it with another PHP version listed in the [support matrix](microsoft-php-drivers-for-sql-server-support-matrix.md) if that repository offers a different one.

The SQLSRV and PDO_SQLSRV drivers use the same ODBC dependency and can be installed independently. Install both extensions if your application uses both drivers.

At the end of the procedure, the CLI and selected web-server runtime should load both extensions, both PHP APIs should be available, and the sample for each API should return the database server version.

## Before you start

1. Choose a PHP version supported by the current driver release and available for your operating-system release.

1. Decide which PHP runtimes you need:

   - CLI for scripts, scheduled tasks, or workers.
   - Apache module for Apache-hosted PHP.
   - PHP-FPM for nginx or an Apache FastCGI configuration.

1. Confirm the operating-system architecture. Released Linux archives contain x64 binaries and released macOS archives contain Arm64 binaries. PIE builds for the PHP installation on the computer and is the primary installation path in this article.

1. Install the Microsoft ODBC Driver and the unixODBC development package. The Microsoft package handles database communication. The unixODBC package supplies headers required to build SQLSRV and PDO_SQLSRV.

1. Plan to restart every web server, PHP-FPM pool, and long-running worker after installing or changing an extension.

The platform package manager installs PHP and native prerequisites. PIE builds and installs the PHP extensions. PHP configuration controls whether each runtime loads them. The ODBC driver handles database communication and encryption after the extension loads.

## Install the drivers with PIE

[PIE](https://github.com/php/pie), the PHP Installer for Extensions, is the supported extension installer for current driver releases. It replaces the deprecated [PHP Extension Community Library (PECL)](https://pecl.php.net/) workflow.

Complete the platform prerequisites in the following sections before installing the drivers. Then install PIE and each driver separately:

### Install PIE

PIE runs on PHP 8.1 and later versions. It can install an extension into another PHP installation when you select that installation's development tools.

Install `curl` and the [GitHub CLI](https://cli.github.com/) before continuing. The platform sections include `curl` where a minimal installation might not provide it.

Download the PIE PHP archive (PHAR), and verify it with the [GitHub command-line interface (CLI)](https://cli.github.com/) before installing or running it:

```bash
pie_tmp=$(mktemp -d)
curl -fL --output "$pie_tmp/pie.phar" https://github.com/php/pie/releases/latest/download/pie.phar
gh attestation verify --owner php "$pie_tmp/pie.phar"
```

Continue only if verification succeeds:

```bash
sudo install -m 755 "$pie_tmp/pie.phar" /usr/local/bin/pie
rm "$pie_tmp/pie.phar"
rmdir "$pie_tmp"
unset pie_tmp
```

The temporary directory is private to your account. If download or verification fails, don't install or run the PHAR. Remove the temporary file and directory before retrying.

Confirm that the installed PHAR runs with the PHP executable you intend to use:

```bash
php /usr/local/bin/pie --version
```

### Install the SQLSRV and PDO_SQLSRV drivers

On Linux and macOS, PIE checks for the compiler and PHP development tools it needs. Install the platform ODBC development package and a C++ compiler before running PIE. Run PIE from an interactive terminal. It can request your `sudo` password when it needs permission to install files into system directories.

PIE targets the PHP installation used to invoke it. For a versioned installation, add `--with-php-config` to each install command to select its development tools. For example, use `pie install --with-php-config=/usr/bin/php-config8.4 microsoft/sqlsrv` when the intended tool is `/usr/bin/php-config8.4`. Apply the same option to `microsoft/pdo_sqlsrv`. Alpine uses versioned executable names; follow the [Alpine commands](#install-on-alpine) instead of the unversioned commands in this section.

Run one command for each extension. These commands use PIE's default PHP installation:

```bash
pie install microsoft/sqlsrv
pie install microsoft/pdo_sqlsrv
```

PIE attempts to enable each extension through the platform's PHP configuration tools or an `extension=` entry, and warns if it can't. PHP uses a server application programming interface (SAPI) for each execution environment. On Debian, Ubuntu, and SUSE, the command-line, Apache, and [PHP FastCGI Process Manager (PHP-FPM)](https://www.php.net/manual/en/install.fpm.php) configurations can differ. A successful command-line installation doesn't confirm that a web server loads the drivers.

Check `php --ini` for the command line. Before creating a temporary `phpinfo()` page for the web server, [restrict diagnostic-page access](loading-the-php-sql-driver.md#protect-web-diagnostic-pages). Check the **Loaded Configuration File** and scanned `.ini` files for each runtime. If the entries are missing, follow [Load the drivers on Linux and macOS](loading-the-php-sql-driver.md#load-the-drivers-on-linux-and-macos) to add them in the correct load order. Restart the web server or PHP-FPM after configuration changes. Red Hat commonly shares `php.ini` across SAPIs, but verify the active configuration before relying on one PIE run.

## Install on your platform

Select your operating system and web server configuration. Complete the installation steps in that tab, then follow [Testing your installation](#testing-your-installation).

# [Ubuntu (Apache)](#tab/ubuntu-apache)

<a id="installing-on-ubuntu"></a>

## Install on Ubuntu

> [!NOTE]
> The following commands use the packages.sury.org PHP repository so that you can select a supported PHP version. Use the official Microsoft instructions to install the ODBC driver for your Ubuntu release.

### Step 1. Install PHP (Ubuntu)

```bash
sudo apt-get update
sudo apt-get install ca-certificates apt-transport-https curl gnupg2 lsb-release -y
sudo install -d -m 755 /etc/apt/keyrings
sudo curl -fsSL https://packages.sury.org/php/apt.gpg -o /etc/apt/keyrings/php.gpg
sudo chmod 644 /etc/apt/keyrings/php.gpg
echo "deb [signed-by=/etc/apt/keyrings/php.gpg] https://packages.sury.org/php/ $(lsb_release -sc) main" | sudo tee /etc/apt/sources.list.d/php.list
sudo apt-get update
sudo apt-get install php8.4 php8.4-dev php8.4-xml -y
```

### Step 2. Install prerequisites (Ubuntu)

Follow [Install the Microsoft ODBC Driver for SQL Server on Linux](../../connect/odbc/linux-mac/installing-the-microsoft-odbc-driver-for-sql-server.md), then install the development headers.

```bash
sudo apt-get install unixodbc-dev g++ make git -y
```

### Step 3. <a name="step-3-install-the-php-drivers-for-microsoft-sql-server-with-pecl-ubuntu"></a>Install the PHP drivers for Microsoft SQL Server with PIE (Ubuntu)

Follow [Install the drivers with PIE](#install-the-drivers-with-pie), adding `--with-php-config=/usr/bin/php-config8.4` to both install commands. Check the command-line configuration files with `php8.4 --ini`.

```bash
/usr/local/bin/pie install --with-php-config=/usr/bin/php-config8.4 microsoft/sqlsrv
/usr/local/bin/pie install --with-php-config=/usr/bin/php-config8.4 microsoft/pdo_sqlsrv
```

### Step 4. Install Apache and configure driver loading (Ubuntu)

```bash
sudo apt-get install apache2 libapache2-mod-php8.4 -y
sudo a2enmod php8.4
```

### Step 5. Restart Apache and test the sample script (Ubuntu)

```bash
sudo systemctl restart apache2
```

To test your installation, see [Testing your installation](#testing-your-installation) at the end of this document.

# [Ubuntu (PHP-FPM)](#tab/ubuntu-fpm)

<a id="installing-on-ubuntu-with-php-fpm"></a>

## Install on Ubuntu with PHP-FPM

Use PHP-FPM instead of the Apache module if you serve PHP through nginx.

### Step 1. Install PHP (Ubuntu with PHP-FPM)

```bash
sudo apt-get update
sudo apt-get install ca-certificates apt-transport-https curl gnupg2 lsb-release -y
sudo install -d -m 755 /etc/apt/keyrings
sudo curl -fsSL https://packages.sury.org/php/apt.gpg -o /etc/apt/keyrings/php.gpg
sudo chmod 644 /etc/apt/keyrings/php.gpg
echo "deb [signed-by=/etc/apt/keyrings/php.gpg] https://packages.sury.org/php/ $(lsb_release -sc) main" | sudo tee /etc/apt/sources.list.d/php.list
sudo apt-get update
sudo apt-get install php8.4 php8.4-dev php8.4-fpm php8.4-xml -y
```

### Step 2. Install prerequisites (Ubuntu with PHP-FPM)

Follow [Install the Microsoft ODBC Driver for SQL Server on Linux](../../connect/odbc/linux-mac/installing-the-microsoft-odbc-driver-for-sql-server.md), then install the development headers.

```bash
sudo apt-get install unixodbc-dev g++ make git -y
```

### Step 3. <a name="step-3-install-the-php-drivers-for-microsoft-sql-server-with-pecl-ubuntu-with-php-fpm"></a>Install the PHP drivers for Microsoft SQL Server with PIE (Ubuntu with PHP-FPM)

Follow [Install the drivers with PIE](#install-the-drivers-with-pie), adding `--with-php-config=/usr/bin/php-config8.4` to both install commands. Confirm that the FPM SAPI loads the drivers with `sudo php-fpm8.4 -m`, which lists the extensions that PHP-FPM loads, or with a temporary `phpinfo()` page after [restricting diagnostic-page access](loading-the-php-sql-driver.md#protect-web-diagnostic-pages). Don't use `php-fpm8.4 -tt` for this check. That command only validates the FPM configuration file and reports success whether or not the drivers are loaded.

```bash
/usr/local/bin/pie install --with-php-config=/usr/bin/php-config8.4 microsoft/sqlsrv
/usr/local/bin/pie install --with-php-config=/usr/bin/php-config8.4 microsoft/pdo_sqlsrv
```

### Step 4. Install and configure nginx (Ubuntu with PHP-FPM)

```bash
sudo apt-get install nginx -y
```

Edit `/etc/nginx/sites-available/default`. Add `index.php` to the `index` directive, and uncomment the `location ~ \.php$` block so nginx passes PHP requests to the PHP-FPM socket. The `fastcgi_pass` line in the file that nginx installs refers to an older PHP version, so set it to the socket for the version you installed:

```text
index index.html index.htm index.nginx-debian.html index.php;

...

location ~ \.php$ {
        include snippets/fastcgi-php.conf;
        fastcgi_pass unix:/run/php/php8.4-fpm.sock;
}
```

### Step 5. Restart nginx and test the sample script (Ubuntu with PHP-FPM)

```bash
sudo systemctl restart php8.4-fpm
sudo systemctl restart nginx
```

To test your installation, see [Testing your installation](#testing-your-installation) at the end of this document.

# [Red Hat](#tab/redhat)

<a id="installing-on-red-hat"></a>

## Install on Red Hat

> [!NOTE]
> These commands use the stock PHP 8.3 packages on Red Hat Enterprise Linux 10. On Red Hat Enterprise Linux 9, first provide PHP 8.3, 8.4, or 8.5 with matching `php-cli`, `php-devel`, `php-pdo`, `php-xml`, and `php-fpm` packages. The default PHP version on Red Hat Enterprise Linux 9 doesn't meet the current drivers' requirements. Skip the PHP installation command in step 1, and use the package and service names for your PHP installation in the remaining steps.

### Step 1. Install PHP (Red Hat)

On Red Hat Enterprise Linux 10, install PHP and its development package:

```bash
sudo dnf install php php-cli php-devel php-pdo php-xml -y
```

On either Red Hat release, check that your PHP version is listed in the [support matrix](microsoft-php-drivers-for-sql-server-support-matrix.md#php-version-support):

```bash
php -v
```

### Step 2. Install prerequisites (Red Hat)

Install the current Microsoft ODBC Driver 18 for SQL Server package for your Red Hat release by following [Install the Microsoft ODBC Driver for SQL Server on Linux](../../connect/odbc/linux-mac/installing-the-microsoft-odbc-driver-for-sql-server.md). Then install the ODBC development headers and build tools.

On Red Hat Enterprise Linux 10, enable the CodeReady Linux Builder repository before installing `unixODBC-devel`. On a system registered with Red Hat Subscription Management, run:

```bash
sudo subscription-manager repos --enable=codeready-builder-for-rhel-10-$(arch)-rpms
```

On cloud images that use Red Hat Update Infrastructure (RHUI), use the cloud provider's CodeReady repository instead. Check the enabled repositories, then install the development packages:

```bash
sudo dnf repolist --enabled
sudo dnf install curl unixODBC-devel gcc gcc-c++ make autoconf pkgconf-pkg-config git -y
```

### Step 3. <a name="step-3-install-the-php-drivers-for-microsoft-sql-server-with-pecl-red-hat"></a>Install the PHP drivers for Microsoft SQL Server with PIE (Red Hat)

Follow [Install the drivers with PIE](#install-the-drivers-with-pie), then restart the PHP process that uses the extensions.

```bash
/usr/local/bin/pie install microsoft/sqlsrv
/usr/local/bin/pie install microsoft/pdo_sqlsrv
```

### Step 4. Install Apache (Red Hat)

Red Hat serves PHP through PHP-FPM. Install both Apache and PHP-FPM:

```bash
sudo dnf install httpd php-fpm -y
```

Red Hat Enterprise Linux enables SELinux enforcement by default. If SELinux is enforcing and Apache needs to reach a database over the network, allow that connection persistently across reboots:

```bash
sudo setsebool -P httpd_can_network_connect_db 1
```

Check the current value first with `getsebool httpd_can_network_connect_db`, and skip the command if it's already `on` or if SELinux isn't enforcing on your system.

### Step 5. Restart Apache and test the sample script (Red Hat)

Restart PHP-FPM to load the extensions, and restart Apache:

```bash
sudo systemctl restart php-fpm httpd
```

To test your installation, see [Testing your installation](#testing-your-installation) at the end of this document.

# [Debian](#tab/debian)

<a id="installing-on-debian"></a>

## Install on Debian

Use Debian's PHP packages, or add a repository that provides a supported PHP version.

### Step 1. Install PHP (Debian)

```bash
sudo apt-get update
sudo apt-get install ca-certificates curl php8.4 php8.4-dev php8.4-xml php8.4-cli -y
```

> [!NOTE]
> This command installs PHP 8.4, the version that Debian's current stable release (trixie) packages natively. On an older Debian release, use the [packages.sury.org](https://packages.sury.org/php/) repository for that release, or another repository that provides a PHP version listed in the [support matrix](microsoft-php-drivers-for-sql-server-support-matrix.md).

### Step 2. Install prerequisites (Debian)

Follow [Install the Microsoft ODBC Driver for SQL Server on Linux](../../connect/odbc/linux-mac/installing-the-microsoft-odbc-driver-for-sql-server.md), then install the development headers.

```bash
sudo apt-get install unixodbc-dev gcc g++ make autoconf pkg-config git -y
```

### Step 3. <a name="step-3-install-the-php-drivers-for-microsoft-sql-server-with-pecl-debian"></a>Install the PHP drivers for Microsoft SQL Server with PIE (Debian)

Follow [Install the drivers with PIE](#install-the-drivers-with-pie), adding `--with-php-config=/usr/bin/php-config8.4` to both install commands.

```bash
/usr/local/bin/pie install --with-php-config=/usr/bin/php-config8.4 microsoft/sqlsrv
/usr/local/bin/pie install --with-php-config=/usr/bin/php-config8.4 microsoft/pdo_sqlsrv
```

### Step 4. Install Apache and configure driver loading (Debian)

```bash
sudo apt-get install apache2 libapache2-mod-php8.4 -y
sudo a2enmod php8.4
```

### Step 5. Restart Apache and test the sample script (Debian)

```bash
sudo systemctl restart apache2
```

To test your installation, see [Testing your installation](#testing-your-installation) at the end of this document.

# [SUSE](#tab/suse)

<a id="installing-on-suse"></a>

## Install on SUSE

These commands use the PHP 8 packages provided by SUSE Linux Enterprise Server 15 SP7. For other SUSE releases, check that the available PHP version is listed in the [support matrix](microsoft-php-drivers-for-sql-server-support-matrix.md).

### Step 1. Install PHP (SUSE)

Install PHP and the development package from the repositories configured for your SUSE release:

```bash
sudo zypper install php8 php8-devel php8-pdo php8-dom php8-iconv php8-openssl
```

Check `php -v` before continuing. If it reports an unsupported PHP version, select a repository that provides a supported version for your SUSE release.

### Step 2. Install prerequisites (SUSE)

Follow [Install the Microsoft ODBC Driver for SQL Server on Linux](../../connect/odbc/linux-mac/installing-the-microsoft-odbc-driver-for-sql-server.md), then install the development tools and ODBC headers:

```bash
sudo zypper install curl unixODBC-devel gcc gcc-c++ make autoconf gawk pkg-config git
```

### Step 3. <a name="step-3-install-the-php-drivers-for-microsoft-sql-server-with-pecl-suse"></a>Install the PHP drivers for Microsoft SQL Server with PIE (SUSE)

Follow [Install the drivers with PIE](#install-the-drivers-with-pie).

```bash
/usr/local/bin/pie install microsoft/sqlsrv
/usr/local/bin/pie install microsoft/pdo_sqlsrv
```

### Step 4. Install Apache and configure driver loading (SUSE)

```bash
sudo zypper install apache2 apache2-mod_php8
sudo a2enmod php8
```

SUSE uses a separate `php.ini` file for the Apache SAPI. Before creating a temporary `phpinfo()` page, [restrict diagnostic-page access](loading-the-php-sql-driver.md#protect-web-diagnostic-pages). Use the page to identify the active configuration and scanned `.ini` files. Check those files for existing driver entries before adding these lines:

```ini
extension=sqlsrv.so
extension=pdo_sqlsrv.so
```

If PHP loads PDO from a scanned `.ini` file, place the PDO_SQLSRV entry in a later scanned file, not in `php.ini`. For more information, see [Load the drivers on Linux and macOS](loading-the-php-sql-driver.md#load-the-drivers-on-linux-and-macos).

### Step 5. Restart Apache and test the sample script (SUSE)

```bash
sudo systemctl restart apache2
```

To test your installation, see [Testing your installation](#testing-your-installation) at the end of this document.

# [Alpine](#tab/alpine)

<a id="installing-on-alpine"></a>

## Install on Alpine

Use the same versioned PHP packages for the command line and Apache.

### Step 1. Install PHP (Alpine)

`php84` is in Alpine's `community` repository. Enable it if it isn't already, then install it:

```bash
sudo sed -i '/^#.*\/community/s/^#//' /etc/apk/repositories
sudo apk update
sudo apk add php84 php84-dev php84-pdo php84-opcache php84-phar php84-openssl php84-iconv php84-zip curl
```

> [!NOTE]
> This command uses `php84`, which is listed in the Alpine v3.22 community package index. Check your release's package index for the exact package name and repository before installing. The PHP version packaged by Alpine can differ by release.

Use `php84` for the command-line checks in this article. The versioned package doesn't provide the unversioned `php` command.

### Step 2. Install prerequisites (Alpine)

Follow [Install the Microsoft ODBC Driver for SQL Server on Linux](../../connect/odbc/linux-mac/installing-the-microsoft-odbc-driver-for-sql-server.md#18), and select the **Alpine** tab. Then install the compiler, PHP build tools, and ODBC development headers:

```bash
sudo apk add unixodbc-dev gcc g++ make autoconf libtool pkgconf
```

### Step 3. <a name="step-3-install-the-php-drivers-for-microsoft-sql-server-with-pecl-alpine"></a>Install the PHP drivers for Microsoft SQL Server with PIE (Alpine)

Complete [Install PIE](#install-pie) before continuing. Invoke the verified PHAR at `/usr/local/bin/pie` by using Alpine's versioned PHP executable and development tools:

```bash
php84 /usr/local/bin/pie install --with-php-config=/usr/bin/php-config84 --with-phpize-path=/usr/bin/phpize84 microsoft/sqlsrv
php84 /usr/local/bin/pie install --with-php-config=/usr/bin/php-config84 --with-phpize-path=/usr/bin/phpize84 microsoft/pdo_sqlsrv
```

### Step 4. Install Apache and configure driver loading (Alpine)

```bash
sudo apk add apache2 php84-apache2
```

### Step 5. Restart Apache and test the sample script (Alpine)

```bash
sudo rc-service apache2 restart
```

To test your installation, see [Testing your installation](#testing-your-installation) at the end of this document.

# [macOS](#tab/macos)

<a id="installing-on-macos"></a>

## Install on macOS

Use Homebrew packages for PHP, unixODBC, and Apache.

### Step 1. Install PHP (macOS)

Install Homebrew, then install PHP, `unixODBC`, and OpenSSL 3:

```bash
brew install php unixodbc openssl@3
```

### Step 2. Install prerequisites (macOS)

Follow the instructions in [Install the Microsoft ODBC Driver for SQL Server on macOS](../../connect/odbc/linux-mac/install-microsoft-odbc-driver-sql-server-macos.md). Confirm that the PHP development tools and `phpize` are available:

```bash
php --version
phpize --version
```

On Apple silicon, if PIE can't find `sql.h`, set the Homebrew `unixODBC` include and library paths.

```bash
export CPPFLAGS="-I$(brew --prefix unixodbc)/include"
export LDFLAGS="-L$(brew --prefix unixodbc)/lib"
```

### Step 3. <a name="step-3-install-the-php-drivers-for-microsoft-sql-server-with-pecl-macos"></a>Install the PHP drivers for Microsoft SQL Server with PIE (macOS)

Follow the instructions in [Install the drivers with PIE](#install-the-drivers-with-pie).

```bash
/usr/local/bin/pie install microsoft/sqlsrv
/usr/local/bin/pie install microsoft/pdo_sqlsrv
```

### Step 4. Install Apache and configure driver loading (macOS)

Install Apache from Homebrew:

```bash
brew install httpd
```

Homebrew's `php` formula reports the exact `LoadModule` line for the PHP version it installed. Run this command to see it:

```bash
brew info php
```

Add the reported `LoadModule` line and a handler for `.php` files to Homebrew's `httpd.conf`, then confirm `DirectoryIndex` includes `index.php`:

```bash
echo "LoadModule php_module $(brew --prefix php)/lib/httpd/modules/libphp.so" >> "$(brew --prefix)/etc/httpd/httpd.conf"
(echo "<FilesMatch \.php\$>"; echo "SetHandler application/x-httpd-php"; echo "</FilesMatch>";) >> "$(brew --prefix)/etc/httpd/httpd.conf"
```

Edit `$(brew --prefix)/etc/httpd/httpd.conf` and add `index.php` to the `DirectoryIndex` directive if it's missing.

### Step 5. Restart Apache and test the sample script (macOS)

```bash
brew services restart httpd
```

If a connection reports `OpenSSL library could not be loaded` and Homebrew's default OpenSSL is version 4, make the installed OpenSSL 3 libraries available when you launch PHP. After creating `testsql.php` as described in [Testing your installation](#testing-your-installation), run it with:

```bash
DYLD_LIBRARY_PATH="$(brew --prefix openssl@3)/lib" php testsql.php
```

For the Apache test, stop the Homebrew service and launch Apache in the foreground with the same library path:

```bash
brew services stop httpd
DYLD_LIBRARY_PATH="$(brew --prefix openssl@3)/lib" "$(brew --prefix httpd)/bin/httpd" -D FOREGROUND
```

Keep that terminal open while testing, then press **Ctrl+C** to stop Apache. The library path applies only to the launched process and its children. Setting it in a shell doesn't configure a service started by `brew services`. Configure the environment of the process that starts your web server before returning to service-based hosting. Don't replace PHP's OpenSSL 4 libraries or disable connection encryption.

Homebrew's `httpd` formula serves `$(brew --prefix)/var/www` on port 8080 by default. To test your installation, see [Testing your installation](#testing-your-installation) at the end of this document.

---

## Legacy PECL installation

PECL is deprecated for current driver releases. Use PIE for new installations and upgrades. If an existing deployment is pinned to PECL, keep its package versions aligned with the PHP runtime and plan a move to PIE.

<a id="testing-your-installation"></a>

## Test your installation

Verify the command-line runtime. On Alpine, replace `php` with `php84`. For a versioned PHP installation on another distribution, use its corresponding executable.

```bash
php --ri sqlsrv
php --ri pdo_sqlsrv
php -m | grep -E "sqlsrv|pdo_sqlsrv"
```

Both `--ri` commands should print module information that includes `ExtensionVer`. The module list should contain separate `sqlsrv` and `pdo_sqlsrv` lines.

Before testing a connection, verify both PHP API surfaces with `verify-php-drivers.php`:

```php
<?php
$checks = [
    'SQLSRV' => extension_loaded('sqlsrv') && function_exists('sqlsrv_connect'),
    'PDO_SQLSRV' => extension_loaded('pdo_sqlsrv')
        && in_array('sqlsrv', PDO::getAvailableDrivers(), true),
];

$failed = false;
foreach ($checks as $name => $loaded) {
    echo "$name: " . ($loaded ? 'loaded' : 'missing') . PHP_EOL;
    $failed = $failed || !$loaded;
}

exit($failed ? 1 : 0);
```

Expected output:

```output
SQLSRV: loaded
PDO_SQLSRV: loaded
```

For PHP-FPM or a web server, [restrict diagnostic-page access](loading-the-php-sql-driver.md#protect-web-diagnostic-pages) before creating a temporary `phpinfo()` page. Confirm the **Loaded Configuration File**, **extension_dir**, and both driver sections. Restart PHP-FPM or the web server after changing configuration or replacing an extension. Remove the page after verification.

To test a live connection, create a file called `testsql.php` in your system's document root. This path is `/var/www/html` on Ubuntu, Debian, and Red Hat, `/srv/www/htdocs` on SUSE, `/var/www/localhost/htdocs` on Alpine, or `$(brew --prefix)/var/www` on macOS. Set the `SQL_SERVER_NAME`, `SQL_DATABASE`, `SQL_UID`, and `SQL_PWD` environment variables for the PHP process before running the script. Don't hardcode credentials in a file under the document root.

For a command-line test, use the [interactive prompts](upgrade-driver.md#verify-database-behavior-before-and-after-the-upgrade) to set the environment variables without entering a password in shell history.

For an Apache module test, keep credentials out of the site's generally readable configuration file:

1. Create a private file outside the document root. Use an unused file name in Apache's configuration directory. On Linux:

   ```bash
   sudo install -o root -g root -m 600 /dev/null /etc/apache2/php-sql-driver-test.conf
   ```

   On Red Hat, replace `/etc/apache2` with `/etc/httpd`. On macOS, create the file as the account that starts Homebrew's Apache:

   ```bash
   install -m 600 /dev/null "$(brew --prefix)/etc/httpd/php-sql-driver-test.conf"
   ```

   Only the file owner and root can read these settings. The account that starts Apache must be able to read the file.

1. Edit the private file in a text editor, and add these entries:

   ```apache
   SetEnv SQL_SERVER_NAME "tcp:<server>,1433"
   SetEnv SQL_DATABASE "<database>"
   SetEnv SQL_UID "<user_id>"
   SetEnv SQL_PWD "<password>"
   ```

   Within the double-quoted arguments, write `\\` for each literal backslash and `\"` for each literal double quote. Preserve leading and trailing spaces inside the quotes. Apache also substitutes `${NAME}` references. If a value contains that sequence or a line break, use the command-line test instead. For more information, see [Apache configuration quoting and escaping](https://httpd.apache.org/docs/2.4/configuring.html#syntax).

1. Inside the site's virtual host, add a `Files` block that includes the private file only for `testsql.php`. If no virtual host is configured, place the block in the main server configuration. Replace the placeholder with the private file's absolute path:

   ```apache
   <Files "testsql.php">
       Include "<absolute-path-to-private-file>"
   </Files>
   ```

1. Restart Apache. After testing, remove the `Files` block you added, delete the private file, and restart Apache again. Keep unrelated site settings.

PHP-FPM clears inherited environment variables by default, and nginx doesn't pass its environment to PHP-FPM. Use the following Ubuntu or Red Hat procedure only with an isolated test PHP-FPM service and pool. Don't add test credentials to a service or pool that runs other applications: every script in that pool can read them. If you use shared hosting, use the command-line test instead.

1. Create a root-owned file outside the document root, such as `/etc/php-sql-driver-test.env`, and restrict access to it:

   ```bash
   sudo touch /etc/php-sql-driver-test.env
   sudo chmod 600 /etc/php-sql-driver-test.env
   ```

   Add the connection settings to the file:

   ```ini
   SQL_SERVER_NAME="tcp:<server>,1433"
   SQL_DATABASE="<database>"
   SQL_UID="<user_id>"
   SQL_PWD="<password>"
   ```

   Keep each setting on one line. Within the double quotes, write `\\` for each literal backslash and `\"` for each literal double quote. Dollar signs and backticks aren't evaluated as shell expressions. Edit the file directly rather than running or sourcing it in a shell. For more information, see [systemd EnvironmentFile syntax](https://www.freedesktop.org/software/systemd/man/latest/systemd.exec.html#EnvironmentFile=).

1. Run `sudo systemctl edit php8.4-fpm` on Ubuntu or `sudo systemctl edit php-fpm` on Red Hat. Add this systemd override:

   ```ini
   [Service]
   EnvironmentFile=/etc/php-sql-driver-test.env
   ```

1. Add these entries to the active PHP-FPM pool configuration. Common locations are `/etc/php/8.4/fpm/pool.d/www.conf` on Ubuntu and `/etc/php-fpm.d/www.conf` on Red Hat:

   ```ini
   env[SQL_SERVER_NAME] = $SQL_SERVER_NAME
   env[SQL_DATABASE] = $SQL_DATABASE
   env[SQL_UID] = $SQL_UID
   env[SQL_PWD] = $SQL_PWD
   ```

1. Reload systemd and restart the applicable PHP-FPM service:

   ```bash
   sudo systemctl daemon-reload
   sudo systemctl restart php8.4-fpm
   ```

   On Red Hat, replace `php8.4-fpm` with `php-fpm`.

After testing, remove the temporary environment file, the `EnvironmentFile=` entry you added, the test pool entries, and `testsql.php`. Delete the service override file only if you created it for this test and it contains no other settings. Keep all preexisting overrides and pool settings. Reload systemd and restart PHP-FPM again after removing the test configuration.

### SQLSRV example

Keep the environment values unchanged. Each example rejects missing or empty settings before connecting. Each example then doubles closing braces and adds outer braces before passing the database name, user name, and password to the driver. This encoding preserves literal braces and prevents semicolons from separating connection options.

```php
<?php
$serverName = getenv('SQL_SERVER_NAME');
$databaseName = getenv('SQL_DATABASE');
$uid = getenv('SQL_UID');
$pwd = getenv('SQL_PWD');

if ($serverName === false || $serverName === ''
    || $databaseName === false || $databaseName === ''
    || $uid === false || $uid === ''
    || $pwd === false || $pwd === '') {
    handle_failure("Set SQL_SERVER_NAME, SQL_DATABASE, SQL_UID, and SQL_PWD.");
}

$databaseName = '{' . str_replace('}', '}}', $databaseName) . '}';
$uid = '{' . str_replace('}', '}}', $uid) . '}';
$pwd = '{' . str_replace('}', '}}', $pwd) . '}';
$connectionOptions = array(
    "Database" => $databaseName,
    "UID" => $uid,
    "PWD" => $pwd,
    "Encrypt" => 1,
    "TrustServerCertificate" => 0
);

function handle_failure($message) {
    error_log($message);
    http_response_code(500);
    echo "<h1>Failure</h1>";
    echo "An error occurred. Check the server error log for details.";
    exit(1);
}

function format_errors($errors) {
    $lines = [];
    foreach ($errors as $error) {
        $lines[] = "SQLSTATE: {$error['SQLSTATE']}, code: {$error['code']}, message: {$error['message']}";
    }
    return implode(PHP_EOL, $lines);
}

$conn = sqlsrv_connect($serverName, $connectionOptions);
if ($conn === false) {
    handle_failure(format_errors(sqlsrv_errors()));
}

$tsql = "SELECT @@Version AS SQL_VERSION";

$stmt = sqlsrv_query($conn, $tsql);
if ($stmt === false) {
    handle_failure(format_errors(sqlsrv_errors()));
}

$row = sqlsrv_fetch_array($stmt, SQLSRV_FETCH_ASSOC);
if ($row === null || $row === false) {
    handle_failure("Query returned no rows.");
}

echo "<h1>Success</h1>";
echo htmlspecialchars($row['SQL_VERSION']);

sqlsrv_free_stmt($stmt);
sqlsrv_close($conn);
?>
```

### PDO_SQLSRV example

```php
<?php
$serverName = getenv('SQL_SERVER_NAME');
$databaseName = getenv('SQL_DATABASE');
$uid = getenv('SQL_UID');
$pwd = getenv('SQL_PWD');

if ($serverName === false || $serverName === ''
    || $databaseName === false || $databaseName === ''
    || $uid === false || $uid === ''
    || $pwd === false || $pwd === '') {
    handle_failure(new RuntimeException("Set SQL_SERVER_NAME, SQL_DATABASE, SQL_UID, and SQL_PWD."));
}

$databaseName = '{' . str_replace('}', '}}', $databaseName) . '}';
$uid = '{' . str_replace('}', '}}', $uid) . '}';
$pwd = '{' . str_replace('}', '}}', $pwd) . '}';

function handle_failure($exception) {
    error_log($exception->getMessage());
    http_response_code(500);
    echo "<h1>Failure</h1>";
    echo "An error occurred. Check the server error log for details.";
    exit(1);
}

try {
    $conn = new PDO(
        "sqlsrv:server=$serverName;Database=$databaseName;Encrypt=1;TrustServerCertificate=0",
        $uid,
        $pwd
    );
    $conn->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);

    $tsql = "SELECT @@Version AS SQL_VERSION";

    $stmt = $conn->query($tsql);
    $row = $stmt->fetch(PDO::FETCH_ASSOC);
    if ($row === false) {
        throw new RuntimeException("Query returned no rows.");
    }

    echo "<h1>Success</h1>";
    echo htmlspecialchars($row['SQL_VERSION']);
} catch (Throwable $exception) {
    handle_failure($exception);
}

unset($stmt);
unset($conn);
?>
```

Point your browser to `http://localhost/testsql.php` (`http://localhost:8080/testsql.php` on macOS). A successful connection displays the database server's version string. Both examples write error details to the PHP error log. If the page displays a failure, check that log.

To compare command-line and web-server behavior, set the same environment variables for your terminal and run:

```bash
php testsql.php
```

If running from the command line succeeds but nothing appears in your browser, check the [Apache log files](https://httpd.apache.org/docs/current/logs.html). For more help, see [Support resources for the PHP SQL driver](support-resources-for-the-php-sql-driver.md).

Evaluate the connection programs separately for SQL Server, Azure SQL Database, Azure SQL Managed Instance, Azure Synapse Analytics, SQL database in Microsoft Fabric, and Warehouse in Microsoft Fabric. The selected authentication method, connection options, and query must be supported by the target. Installation is client-side. A successful extension check doesn't prove that a target accepts the credentials, trusts the client, or supports every database feature.

## Troubleshoot installation

| Symptom | Owning layer | Corrective action |
|---|---|---|
| PIE can't find `phpize`, `php-config`, or headers | PHP development packages | Install the development package for the target PHP version and pass the matching `--with-php-config` option |
| Compilation fails because a C++ compiler is missing | Operating-system build tools | Install the platform C++ compiler package, such as `g++` or `gcc-c++` |
| Compilation can't find `sql.h` | ODBC development package or compiler paths | Install `unixodbc-dev` or `unixODBC-devel`; on Apple silicon, set the Homebrew include and library flags |
| `php --ri` says the extension isn't present | PHP configuration | Inspect the loaded `php.ini` and scan directory, add the correct entry, and restart the process |
| PDO reports `could not find driver` | PDO_SQLSRV loading or PDO load order | Confirm PDO loads first and PDO_SQLSRV uses a later scanned configuration file |
| CLI works but Apache, nginx, or PHP-FPM fails | SAPI-specific configuration | Compare CLI output with `phpinfo()`, configure the web SAPI, and restart it |
| The browser returns HTTP 502 through nginx | nginx to PHP-FPM routing | Check the `fastcgi_pass` socket against the installed PHP-FPM version and confirm the service is running |
| The browser sample reports missing environment values | Web-server or PHP-FPM environment | Configure the variables for the selected web runtime, then restart it |
| Extensions load but a connection fails | ODBC, network, certificate, authentication, or SQL product | Read the SQLSTATE and native error from the server log; don't rebuild the extension unless the error identifies loading or binary compatibility |

## Remove temporary test configuration

After verification:

1. Delete `testsql.php`, `verify-php-drivers.php`, and any temporary `phpinfo()` page from the document root.

1. Remove temporary credentials and environment-forwarding entries.

1. Reload the service manager when you change a systemd override, then restart PHP-FPM or the web server.

1. Keep the extension entries and installed packages that the application requires.

## Related content

- [Install the PHP drivers for SQL Server on Windows](windows-install.md)
- [Loading the Microsoft Drivers for PHP for SQL Server](loading-the-php-sql-driver.md)
- [Upgrade the PHP drivers for SQL Server](upgrade-driver.md)
- [System requirements for the Microsoft Drivers for PHP for SQL Server](system-requirements-for-the-php-sql-driver.md)
