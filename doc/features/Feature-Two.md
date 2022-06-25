# Calculate Route

In order to show our suppliers where to go, we should of course show them a route on the map. The route is calculated from the current position to a specified position and gets then displayed on the map.
If you don't have a vignette, that's no problem either: simply activate the corresponding setting later on in the settings.

## Requirements

- user has choosen and started a route from the route list
- internet connection is available for the calculation of the route
- the specified end-point is reachable by car
- map page has been initialized to show up the route on it

## Additional Information

Because those requests for the route calculation have to be async, it was sometimes hard to design it in a way it's compatible with the other code using task results.

---

[Back to the overview](./Index.md)
