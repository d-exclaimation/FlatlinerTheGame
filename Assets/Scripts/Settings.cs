//
//  The MIT License (MIT)
//  Copyright © 2020 d-exclaimation
//

using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour {

    public Dropdown crouchMenu;
    public Dropdown jumpMenu;
    public Dropdown shootMenu;
    public Dropdown displayMenu;
    public Slider volumeSlider;
    public Dropdown qualitySlider;

    private readonly KeyCode[] _crouchOptions = { KeyCode.Z, KeyCode.LeftShift, KeyCode.RightShift, KeyCode.C, KeyCode.LeftAlt };
    private readonly KeyCode[] _jumpOptions = { KeyCode.Space, KeyCode.LeftControl, KeyCode.A };
    private readonly KeyCode[] _shootOptions = { KeyCode.F, KeyCode.A, KeyCode.X, KeyCode.Q };
    private Resolution[] _resolutions => Screen.resolutions;
    
    private void Start() {
        // Crouch DropMenu Setup
        setDropdowns(crouchMenu, _crouchOptions.Select(option => option.ToString()).ToList());
        crouchMenu.value = getIndexOf(_crouchOptions, PlayerMovement.crouchKey);
        crouchMenu.RefreshShownValue();
        
        // Jump DropMenu Setup
        setDropdowns(jumpMenu, _jumpOptions.Select(option => option.ToString()).ToList());
        jumpMenu.value = getIndexOf(_jumpOptions, PlayerMovement.jumpKey);
        jumpMenu.RefreshShownValue();
        
        // Shoot DropMenu Setup
        setDropdowns(shootMenu, _shootOptions.Select(option => option.ToString()).ToList());
        shootMenu.value = getIndexOf(_shootOptions, ShootingMechanic.shootKey);
        shootMenu.RefreshShownValue();
        
        
        // Resolutions DropMenu Setup
        setDropdowns(displayMenu, _resolutions.Select(option => option.width + " x " + option.height).ToList());
        displayMenu.value = getResoluteIndex(_resolutions, Screen.currentResolution);
        displayMenu.RefreshShownValue();
        
        // Volume Slider Setup
        volumeSlider.value = AudioManager.defaultVolume ?? 0.14f;
        
        // Quality DropDown Setup
        qualitySlider.value = QualitySettings.GetQualityLevel();
        Debug.Log(QualitySettings.GetQualityLevel());
        qualitySlider.RefreshShownValue();
    }

    public void setQuality(int index) {
        QualitySettings.SetQualityLevel(index);
    }

    public void setFullScreen(bool isOn) {
        Screen.fullScreen = isOn;
    }

    public void setResolutions(int index) {
        var result = _resolutions[index];
        Screen.SetResolution(result.width, result.height, Screen.fullScreen);
    }
    
    public void setVolume(float volume) {
        AudioManager.defaultVolume = volume;
    }
    
    public void setCrouchKey(int index) {
        PlayerMovement.crouchKey = _crouchOptions[index];
    }

    public void setJumpKey(int index) {
        PlayerMovement.jumpKey = _jumpOptions[index];
    }

    public void setShootKey(int index) {
        ShootingMechanic.shootKey = _shootOptions[index];
    }
    
    public void disableSetting() {
        gameObject.SetActive(false);
    }

    private static void setDropdowns(Dropdown menu, List<string> options) {
        menu.ClearOptions();
        menu.AddOptions(options);
    }

    private static int getIndexOf(KeyCode[] options, KeyCode item) {
        for (var i = 0; i < options.Length; i++) {
            if (options[i] == item) {
                return i;
            }
        }

        return -1;
    }

    private static int getResoluteIndex(Resolution[] arr, Resolution item) {
        for (var i = 0; i < arr.Length; i++) {
            if (arr[i].width == item.width && arr[i].height == item.height) {
                return i;
            }
        }

        return -1;
    }
}
