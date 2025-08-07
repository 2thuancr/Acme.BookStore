import { mapEnumToOptions } from '@abp/ng.core';

export enum BookStatus {
  Available = 0,
  Unavailable = 1,
  Reserved = 2,
  Lost = 3,
  Damaged = 4,
}

export const bookStatusOptions = mapEnumToOptions(BookStatus);
