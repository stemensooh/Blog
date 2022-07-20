export class MyConfiguration {

  public urlSftpHub: string = "";
  public urlWindowsServiceHub: string = "";

  constructor() {

  }

  public GetHubSftp(): string {
    return this.urlSftpHub;
  }

  public GetHubWindowsService(): string {
    return this.urlWindowsServiceHub;
  }

  // VARIABLES STATICAS
  static nameMethodSftpHub = 'transferDataSftpHub';
  static nameMethodWindowsservicehub = 'transferDataWindowsServiceHub';
  static tokenValue = 'KKJFJkjfhdjuUhuhYTFt';
  static tokenExpiration = 'asw55f';
  static tokenEnvironment = 'a2uv98a77';
  static tokenRefresh = 'fj56/33';
  static userOnBrowser = 'Qzp++Iss';

  static FormLoginEmail = 'FormLoginEmail';
  static FormLoginIdentificacion = 'FormLoginIdentificacion';
  static FormLoginRecordarme = 'FormLoginRecordarme';
}


export type sourcePage = 'posts' | 'home' | 'about' | 'post-form' | 'search';
