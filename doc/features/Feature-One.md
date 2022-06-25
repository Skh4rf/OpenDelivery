# Device Location

One of the most fundamental features for a navigation system is probably the location detection. It is used to determine the exact position of the device and thus show the user where he is on the map.
In addition, the accuracy of the position determination and a movement threshold value can be set, with which a change can be detected and the new position calculated.

## Requirements

- device has a GPS module
- user allows the application to access the device location
- device is in an environment where it can establish connection to the GPS satellites (e.g. not in a tunnel)
- map page has been initialized to show up the position on it

## Additional Information

Since UWP allows the creation of very secure apps, it was first not easy to grant the application permission to query the location using the permission request in order to avoid tedious manual permission settings.
Luckily we got it solved after a while. If it still doesn't work, we kindly ask you to grant the permissions manually.

---

[Back to the overview](./Index.md)
