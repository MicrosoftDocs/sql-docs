---
title: "Quickstart: Connect Django to SQL Server"
description: Connect a Django application to SQL Server using mssql-django and run database operations with the Django ORM.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: randolphwest
ms.date: 06/22/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: quickstart
ai-usage: ai-assisted
---

# Quickstart: Connect Django to SQL Server

In this quickstart, you create a Django project, connect it to a SQL Server database using `mssql-django`, run migrations, and perform basic data operations with the Django ORM.

## Prerequisites

- Python 3.8 or later. Django 6.0 requires Python 3.12 and later versions.
- Microsoft ODBC Driver 17 or 18 for SQL Server. See [Download ODBC Driver for SQL Server](../../odbc/download-odbc-driver-for-sql-server.md).
- A SQL Server instance or Azure SQL Database with a valid login.

## Step 1: Install mssql-django

Create a virtual environment and install the package:

```bash
python -m venv .venv
```

### [Windows](#tab/windows)

```console
.venv\Scripts\activate
pip install mssql-django
```

### [Linux/macOS](#tab/linux)

```bash
source .venv/bin/activate
pip install mssql-django
```

---

## Step 2: Create a Django project

Create a new Django project and app:

```bash
django-admin startproject myproject
cd myproject
python manage.py startapp myapp
```

## Step 3: Configure the database

Edit `myproject/settings.py` and replace the default `DATABASES` setting.

### Connect to SQL Server

```python
DATABASES = {
    "default": {
        "ENGINE": "mssql",
        "NAME": "<your-database>",
        "USER": "<your-username>",
        "PASSWORD": "<your-password>",
        "HOST": "<your-server>",
        "PORT": "1433",
    },
}
```

[!INCLUDE [trust-server-certificate-caution](includes/trust-server-certificate-caution.md)]

### Connect to Azure SQL Database

```python
DATABASES = {
    "default": {
        "ENGINE": "mssql",
        "NAME": "<your-database>",
        "USER": "<your-username>",
        "PASSWORD": "<your-password>",
        "HOST": "<your-server>.database.windows.net",
        "PORT": "1433",
        "OPTIONS": {
            "driver": "ODBC Driver 18 for SQL Server",
            "extra_params": "Encrypt=yes",
        },
    },
}
```

## Step 4: Define a model

Edit `myapp/models.py`:

```python
from django.db import models

class Product(models.Model):
    name = models.CharField(max_length=100)
    price = models.DecimalField(max_digits=10, decimal_places=2)
    created_at = models.DateTimeField(auto_now_add=True)

    def __str__(self):
        return self.name
```

Add `"myapp"` to `INSTALLED_APPS` in `settings.py`:

```python
INSTALLED_APPS = [
    "django.contrib.admin",
    "django.contrib.auth",
    "django.contrib.contenttypes",
    "django.contrib.sessions",
    "django.contrib.messages",
    "django.contrib.staticfiles",
    "myapp",
]
```

## Step 5: Run migrations

Generate and apply database migrations:

```bash
python manage.py makemigrations myapp
python manage.py migrate
```

Confirm that the `myapp` migration was applied:

```bash
python manage.py showmigrations myapp
```

You should see `[X] 0001_initial`. If you see `[ ] 0001_initial`, rerun `python manage.py migrate myapp` before continuing.

## Step 6: Use the Django ORM

Open the Django shell. The shell is an interactive Python session with your Django project loaded, indicated by the `>>>` prompt.

```bash
python manage.py shell
```

At the `>>>` prompt, import the model:

```python
from myapp.models import Product
```

Create a record:

```python
product = Product.objects.create(name="Widget", price=9.99)
print(f"Created: {product.name} (id={product.pk})")
```

Read records:

```python
for p in Product.objects.all():
    print(f"{p.name}: ${p.price}")
```

Update the record:

```python
product.price = 12.99
product.save()
```

Delete the record:

```python
product.delete()
```

Exit the shell with `exit()`. Alternatively, use <kbd>Ctrl</kbd>+<kbd>Z</kbd> on Windows, or <kbd>Ctrl</kbd>+<kbd>D</kbd> on Linux or macOS.

> [!NOTE]
> If you get an `Invalid object name 'myapp_product'` error, the `myapp_product` table doesn't exist in the database even though Django's migration history claims `0001_initial` is applied. Exit the shell, then reset the migration history and reapply it:
>
> ```bash
> python manage.py migrate myapp zero --fake
> python manage.py migrate myapp
> ```

## Next step

> [!div class="nextstepaction"]
> [mssql-django configuration reference](configuration-reference.md)

## Related content

- [Install mssql-django](installation.md)
- [Django field to SQL Server type mappings](data-type-mappings.md)
- [Django tutorial](https://docs.djangoproject.com/en/stable/intro/tutorial01)
- [mssql-django on GitHub](https://github.com/microsoft/mssql-django)
