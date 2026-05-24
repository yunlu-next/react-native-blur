'use strict';

module.exports = {
  dependency: {
    platforms: {
      windows: {
        sourceDir: './windows',
        projects: [
          {
            projectFile: 'ReactNativeBlur/ReactNativeBlur.csproj',
            directDependency: true,
          },
        ],
      },
    },
  },
};
