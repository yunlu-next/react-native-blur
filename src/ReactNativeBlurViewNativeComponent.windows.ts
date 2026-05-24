import { requireNativeComponent } from 'react-native';
import type { ColorValue, HostComponent, ViewProps } from 'react-native';

export type BlurType =
  | 'xlight'
  | 'light'
  | 'dark'
  | 'extraDark'
  | 'regular'
  | 'prominent'
  | 'systemUltraThinMaterial'
  | 'systemThinMaterial'
  | 'systemMaterial'
  | 'systemThickMaterial'
  | 'systemChromeMaterial'
  | 'systemUltraThinMaterialLight'
  | 'systemThinMaterialLight'
  | 'systemMaterialLight'
  | 'systemThickMaterialLight'
  | 'systemChromeMaterialLight'
  | 'systemUltraThinMaterialDark'
  | 'systemThinMaterialDark'
  | 'systemMaterialDark'
  | 'systemThickMaterialDark'
  | 'systemChromeMaterialDark';

interface NativeProps extends ViewProps {
  blurAmount?: number;
  blurType?: BlurType;
  blurRounds?: number;
  ignoreSafeArea?: boolean;
  overlayColor?: ColorValue;
  reducedTransparencyFallbackColor?: ColorValue;
  tintColor?: ColorValue;
}

export default requireNativeComponent<NativeProps>(
  'ReactNativeBlurView'
) as HostComponent<NativeProps>;
