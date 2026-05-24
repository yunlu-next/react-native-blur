import React from 'react';
import type { ColorValue, StyleProp, ViewStyle } from 'react-native';
import ReactNativeBlurView, {
  type BlurType,
} from './ReactNativeBlurViewNativeComponent.windows';

export interface BlurViewProps {
  /**
   * @description The type of blur effect to apply
   *
   * @default 'xlight'
   */
  blurType?: BlurType;

  /**
   * @description The intensity of the blur effect (0-100)
   *
   * @default 10
   */
  blurAmount?: number;

  /**
   * @description The number of blur interactions to perform for a smoother
   * effect (1-15)
   *
   * @default 5
   *
   * @platform Android
   */
  blurRounds?: number;

  /**
   * @description Fallback color when reduced transparency is enabled
   *
   * @default '#FFFFFF'
   *
   * @platform iOS
   */
  reducedTransparencyFallbackColor?: string;

  /**
   * @description The overlay color to apply on top of the blur effect
   *
   * @default undefined
   */
  overlayColor?: ColorValue;

  /**
   * @description style object for the blur view
   *
   * @default undefined
   */
  style?: StyleProp<ViewStyle>;

  /**
   * @description style object for the blur view
   *
   * @default true
   */
  ignoreSafeArea?: boolean;

  /**
   * @description Child components to render inside the blur view
   *
   * @default undefined
   */
  children?: React.ReactNode;
}

export const BlurView: React.FC<BlurViewProps> = ({
  blurType = 'xlight',
  blurAmount = 10,
  blurRounds = 5,
  reducedTransparencyFallbackColor = '#FFFFFF',
  overlayColor,
  style,
  children,
  ignoreSafeArea = true,
  ...props
}) => {
  return (
    <ReactNativeBlurView
      style={style}
      blurType={blurType}
      blurAmount={blurAmount}
      blurRounds={blurRounds}
      ignoreSafeArea={ignoreSafeArea}
      overlayColor={overlayColor}
      reducedTransparencyFallbackColor={reducedTransparencyFallbackColor}
      {...props}
    >
      {children}
    </ReactNativeBlurView>
  );
};

export default BlurView;
