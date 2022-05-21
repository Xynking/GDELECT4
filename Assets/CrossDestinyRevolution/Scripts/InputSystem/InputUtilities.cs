using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.InputSystem.LowLevel;

namespace CDR.InputSystem
{
    [System.Serializable]
    public static class InputUtilities
    {
        static InputUtilities()
        {
            InputUser.onUnpairedDeviceUsed += (control, ptr) => onUnpairedInputDevicUsedEvent?.Invoke(control.device);
        }

        public static event Action<InputDevice> onUnpairedInputDevicUsedEvent;

        public static InputDevice[] GetAllInputDevices()
        {
            return UnityEngine.InputSystem.InputSystem.devices.ToArray();
        }

        public static InputDevice[] GetAllInputDevices(params InputDevice[] excludedDevices)
        {
            return GetAllInputDevices()?.Except(excludedDevices)?.ToArray();
        }

        public static InputDevice[] GetAllUnpairedInputDevices()
        {
            return InputUser.GetUnpairedInputDevices().ToArray();
        }

        public static InputDevice[] GetAllUnpairedInputDevices(params InputDevice[] excludedDevices)
        {
            return GetAllUnpairedInputDevices()?.Except(excludedDevices)?.ToArray();
        }

        public static T AssignPlayerInput<T>(GameObject gameObject, InputActionAsset inputActionAsset, params InputDevice[] devices) where T : MonoBehaviour, IPlayerInput
        {
            Debug.Assert(inputActionAsset, $"[Input System Error] {inputActionAsset} is not valid!");

            foreach(InputDevice device in devices)
                Debug.Assert(device != null, $"[Input System Error] No Input Device to pair!");

            T playerInput = gameObject.AddComponent<T>();

            playerInput.SetupInput(inputActionAsset, devices);

            return playerInput;
        }

        public static T AssignAIInput<T>(GameObject gameObject) where T : MonoBehaviour, IAIInput
        {
            T aIInput = gameObject.AddComponent<T>();

            aIInput.SetupInput();

            return aIInput;
        }
    }
}