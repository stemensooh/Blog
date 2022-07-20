export interface User {
    uid:             string;
    displayName:     null;
    photoURL:        null;
    email:           string;
    emailVerified:   boolean;
    phoneNumber:     null;
    isAnonymous:     boolean;
    tenantId:        null;
    providerData:    ProviderDatum[];
    apiKey:          string;
    appName:         string;
    authDomain:      string;
    stsTokenManager: StsTokenManager;
    redirectEventId: null;
    lastLoginAt:     string;
    createdAt:       string;
    multiFactor:     MultiFactor;
}

export interface MultiFactor {
    enrolledFactors: any[];
}

export interface ProviderDatum {
    uid:         string;
    displayName: null;
    photoURL:    null;
    email:       string;
    phoneNumber: null;
    providerId:  string;
}

export interface StsTokenManager {
    apiKey:         string;
    refreshToken:   string;
    accessToken:    string;
    expirationTime: number;
}