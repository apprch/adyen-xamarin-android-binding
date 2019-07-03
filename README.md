# Xamarin Android bindings for Adyen Checkout

This repository contains Xamarin Android bindings for Adyen Checkout.

It includes the following libraries:

- com.adyen.checkout.base
- com.adyen.checkout.core
- com.adyen.checkout.core-card
- com.adyen.checkout.util
- com.adyen.checkout.ui
- com.adyen.cse.adyen-cse

The actual .aar files for the libraries are not included in the repository, but can be automatically downloaded from https://jcenter.bintray.com/com/adyen by running the build script:

```powershell
 > ./build.ps1 -Target DownloadAdyenPackages
```

Please note that the bindings are not yet fully tested, and that any use in an actual project is __on your own risk__!
