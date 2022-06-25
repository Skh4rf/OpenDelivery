# Calculate Geocoding

In order to provide a end-point for the route calculation, the address has to be convertet into a geoposition.

## Requirements

- user has choosen a customer with an address to geocode
- internet connection is available for the calculation of the coords
- the address exists and can be found by the api

## Additional Information

Because this API is searching all over the world for this one position, we added a query hint with the coords of Alberschwende to make the search for addresses in Vorarlberg easier.
Later on you will be able to define this hint in the settings.

---

[Back to the overview](./Index.md)
