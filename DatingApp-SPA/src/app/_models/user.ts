import { Photo } from './photo';

export interface User {
  id: number;
  username: string;
  knownAs: string;
  age: number;
  gender: string;
  createdAt: Date;
  lastActive: Date;
  photoUrl: string;
  city: string;
  country: string;
  // Optional properties have to come after mandatory properties
  interests?: string;
  introduction?: string;
  lookingFor?: string;
  photos?: Photo[];
}
