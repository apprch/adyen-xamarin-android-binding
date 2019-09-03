# Xamarin Android bindings for Adyen Checkout

This repository contains Xamarin Android bindings for Adyen Checkout v3.

It includes the following libraries:

- com.adyen.checkout.base-ui
- com.adyen.checkout.base-v3
- com.adyen.checkout.card-base
- com.adyen.checkout.card-ui
- com.adyen.checkout.core-v3
- com.adyen.checkout.cse
- com.adyen.cse.adyen-cse

The actual .aar files for the libraries are not included in the repository, but can be automatically downloaded from https://jcenter.bintray.com/com/adyen by running the build script:

_NOTE_ The aar file for card-ui __is__ checked in and will not be downloaded by the build script. This is because there was a bug in the original file that caused some Android components not to be generated at all (specifically CardView).

```powershell
 > ./build.ps1 -Target DownloadAdyenPackages
```

Please note that the bindings are not yet fully tested, and that any use in an actual project is __on your own risk__!
