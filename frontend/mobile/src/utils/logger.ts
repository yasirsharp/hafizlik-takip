/**
 * Hafizlik Takip - Mobile Logger
 * 
 * Environments:
 * - __DEV__: Development
 * - process.env.NODE_ENV === 'test': Testing
 * - Production
 */

export enum LogLevel {
  DEBUG = 0,
  INFO = 1,
  WARN = 2,
  ERROR = 3,
  NONE = 4
}

class Logger {
  private currentLevel: LogLevel;

  constructor() {
    if (__DEV__) {
      this.currentLevel = LogLevel.DEBUG;
    } else if (process.env.NODE_ENV === 'test') {
      this.currentLevel = LogLevel.WARN;
    } else {
      // In production, we usually only want errors or warnings, and send them to a remote service like Sentry
      this.currentLevel = LogLevel.ERROR;
    }
  }

  public setLevel(level: LogLevel) {
    this.currentLevel = level;
  }

  public debug(message: string, ...args: any[]) {
    if (this.currentLevel <= LogLevel.DEBUG) {
      console.log([DEBUG] , ...args);
    }
  }

  public info(message: string, ...args: any[]) {
    if (this.currentLevel <= LogLevel.INFO) {
      console.info([INFO] , ...args);
    }
  }

  public warn(message: string, ...args: any[]) {
    if (this.currentLevel <= LogLevel.WARN) {
      console.warn([WARN] , ...args);
    }
  }

  public error(message: string, ...args: any[]) {
    if (this.currentLevel <= LogLevel.ERROR) {
      console.error([ERROR] , ...args);
      // TODO: Send to remote error tracking service (e.g., Sentry, Crashlytics) in production
    }
  }
}

export const logger = new Logger();
