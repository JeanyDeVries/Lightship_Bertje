using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Niantic.ARDK.AR.WayspotAnchors;
using Niantic.ARDK.LocationService;
using Niantic.ARDK.AR;
using TMPro;
using Niantic.ARDK.Extensions;

public class LocationManager : MonoBehaviour
{
    [SerializeField] ARSessionManager sessionManager;

    private IARSession _arSession;
    private WayspotAnchorService wayspotAnchorService;

    private float desiredAccuracyInMeters = 0.01f;
    private float updateDistanceInMeters = 0.01f;


    public void StartLocalising()
    {
        _arSession = sessionManager.ARSession;
        var wayspotAnchorsConfiguration = WayspotAnchorsConfigurationFactory.Create();
        var locationService = LocationServiceFactory.Create(_arSession.RuntimeEnvironment);

        // For LocationService, we recommend using a desiredAccuracyInMeters of 0.01f
        // and an updateDistanceInMeters of 0.01f for best results
        locationService.Start(desiredAccuracyInMeters, updateDistanceInMeters);
        wayspotAnchorService = new WayspotAnchorService(_arSession, locationService, wayspotAnchorsConfiguration);

        // Set wayspotAnchorService.LocalizationStateUpdated event handler if we want to track localization state
        wayspotAnchorService.LocalizationStateUpdated += LocalizationStateUpdated;

        // VPS WayspotAnchorService begins localizing, provide instructions for your user on how to localize, etc
    }

    private void LocalizationStateUpdated(LocalizationStateUpdatedArgs args)
    {
        // Handle any changes in localization state as needed. For example,
        // if state is Localized with a FailureReason of None, you app can
        // start creating or restoring anchors
        // ...

        if (args.State == LocalizationState.Failed)
        {
            Debug.Log("localisation failed + " + args.FailureReason.ToString());
            return;
        }
        else{ Debug.Log("localisation succesfull"); };
    }
}
